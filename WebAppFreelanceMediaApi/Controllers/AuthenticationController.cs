using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserService userService, IStaffService staffService, IConfiguration configuration)
        {
            _userService = userService;
            _staffService = staffService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginEntryModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid login data"
                });
            }

            var adminUsername = _configuration["AdminCredentials:Username"];
            var adminPassword = _configuration["AdminCredentials:Password"];

            if (loginModel.UserName == adminUsername && loginModel.Password == adminPassword)
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Admin login successful",
                    UserName = adminUsername,
                    Role = "Admin"
                });
            }

            var user = await _userService.ValidateUserAsync(loginModel.UserName, loginModel.Password);
            if (user != null)
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "User login successful",
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Role = "User"
                });
            }

            var staff = await _staffService.ValidateStaffAsync(loginModel.UserName, loginModel.Password);
            if (staff != null)
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Staff login successful",
                    UserId = staff.StaffId,
                    UserName = staff.UserName,
                    Role = "Staff"
                });
            }

            return BadRequest(new LoginResponse
            {
                Success = false,
                Message = "Invalid username or password"
            });
        }

        [HttpPost("Logout")]
        public ActionResult<LoginResponse> Logout()
        {
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Logout successful"
            });
        }
    }
}
