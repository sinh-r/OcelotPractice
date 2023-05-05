using Identity.API.Enum;
using Identity.API.Interfaces;
using Identity.API.Models.DTO;
using Identity.API.Models.Entities;

namespace Identity.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityBusiness _identityBusiness;

        public UserBusiness(IUserRepository userRepository, IIdentityBusiness identityBusiness)
        {
            _userRepository = userRepository;
            _identityBusiness = identityBusiness;
        }
        public Status Register(UserDto userDto)
        {
            try
            {
                var existingUser = _userRepository.FindUser(userDto.UserName, userDto.Password);

                if (existingUser is null)
                {
                    User user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = userDto.UserName,
                        Password = userDto.Password
                    };

                    _identityBusiness.CreatePasswordHashSalt(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _userRepository.AddUser(user);

                    return Status.Success;
                }
                else
                {
                    return Status.InvalidInput;
                }
            }
            catch (Exception)
            {
                return Status.Exception;
            }
        }

        public Tuple<Status, RefreshTokenModel> Login(UserDto userDto)
        {
            Tuple<Status, RefreshTokenModel> response;

            try
            {
                var existingUser = _userRepository.FindUser(userDto.UserName, userDto.Password);

                if (existingUser is not null)
                {
                    var isAuthenticatedUser = _identityBusiness.AuthenticateUser(existingUser.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

                    if (isAuthenticatedUser)
                    {
                        RefreshTokenModel token = new()
                        {
                            Token = _identityBusiness.CreateToken(existingUser.UserName),
                            RefreshToken = _identityBusiness.CreateRefreshToken(),
                            RefreshTokenExpiry = DateTime.Now.AddDays(2)
                        };

                        response = Tuple.Create(Status.Created, token);
                    }
                    else
                    {
                        response = Tuple.Create(Status.InvalidInput, new RefreshTokenModel());
                    }
                }
                else
                {
                    response = Tuple.Create(Status.InvalidInput, new RefreshTokenModel());
                }
            }
            catch (Exception)
            {
                response = Tuple.Create(Status.Exception, new RefreshTokenModel());
            }

            return response;
        }

        public Tuple<Status, RefreshTokenModel> Refresh(RefreshTokenModel refreshTokenModel)
        {
            Tuple<Status, RefreshTokenModel> response;

            try
            {
                if (refreshTokenModel.RefreshTokenExpiry >= DateTime.Now)
                {
                    var principal = _identityBusiness.GetPrincipalFromExpiredToken(refreshTokenModel.Token);

                    if (principal is not null)
                    {
                        var userName = principal?.Identity?.Name ?? string.Empty;

                        var existingUser = _userRepository.FindUser(userName);

                        if (existingUser is not null)
                        {
                            RefreshTokenModel refreshToken = new()
                            {
                                Token = _identityBusiness.CreateToken(userName),
                                RefreshToken = _identityBusiness.CreateRefreshToken(),
                                RefreshTokenExpiry = DateTime.Now.AddDays(2)
                            };

                            response = Tuple.Create(Status.Created, refreshToken);
                        }
                        else
                        {
                            response = Tuple.Create(Status.InvalidInput, new RefreshTokenModel());
                        }
                    }
                    else
                    {
                        response = Tuple.Create(Status.InvalidInput, new RefreshTokenModel());
                    }
                }
                else
                {
                    response = Tuple.Create(Status.ExpiredRefresh, new RefreshTokenModel());
                }
            }
            catch (Exception)
            {
                response = Tuple.Create(Status.InvalidInput, new RefreshTokenModel());
            }

            return response;
        }
    }
}
