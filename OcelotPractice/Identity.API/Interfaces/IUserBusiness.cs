using Identity.API.Enum;
using Identity.API.Models.DTO;

namespace Identity.API.Interfaces
{
    public interface IUserBusiness
    {
        public Status Register(UserDto userDto);
        public Tuple<Status, RefreshTokenModel> Login(UserDto userDto);

        public Tuple<Status, RefreshTokenModel> Refresh(RefreshTokenModel refreshTokenModel);
    }
}
