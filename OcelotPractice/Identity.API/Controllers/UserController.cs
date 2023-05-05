using Identity.API.Interfaces;
using Identity.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IPrepareResponseBusiness _prepareResponseBusiness;

        public UserController(IUserBusiness userBusiness, IPrepareResponseBusiness prepareResponseBusiness)
        {
            _userBusiness = userBusiness;
            _prepareResponseBusiness = prepareResponseBusiness;
        }

        [HttpPost("Register")]
        public ActionResult Register(UserDto user)
        {
            var status = _userBusiness.Register(user);
            var response = _prepareResponseBusiness.PrepareResponse(
                status,
                "User created successfully!",
                "User already exists!",
                "Error occured while registering user");

            return StatusCode(response.StatusCode, new { response.Status, response.Message });
        }

        [HttpPost("Login")]
        public ActionResult Login(UserDto user)
        {
            var status = _userBusiness.Login(user);
            var response = _prepareResponseBusiness.PrepareResponse(status.Item1,
                "Logged in successfully",
                "Invalid credentials",
                "Error occured");

            return StatusCode(response.StatusCode, new { response.Status, response.Message, Token = status.Item2 });
        }

        [HttpPost("Refresh")]
        public ActionResult Refresh(RefreshTokenModel refreshTokenModel)
        {
            var status = _userBusiness.Refresh(refreshTokenModel);
            var response = _prepareResponseBusiness.PrepareResponse(status.Item1,
                "Refresed successfully!",
                "Invalid refresh token",
                "Error occured while refreshing token",
                "Refresh token has expired please login again");

            return StatusCode(response.StatusCode, new { response.Status, response.Message, Token = status.Item2 });
        }
    }
}
