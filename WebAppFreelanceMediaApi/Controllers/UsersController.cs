using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;
using WebAppFreelanceMediaApi.Services.RepServices;
using WebAppFreelanceMediaApi.ViewModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;

        public UsersController(IUserService userService, IArticleService articleService)
        {
            this._userService = userService;
            _articleService = articleService;
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<UserViewModel>> GetUserByIdAsync(int id)
        {
            UserModel? userModel = await _userService.GetUserByIdAsync(id);
            if (userModel == null)
            {
                ModelState.AddModelError("User", string.Format("User not found"));
                return NotFound(new ValidationProblemDetails(ModelState));
            }

            UserViewModel? userViewModel = UserViewModel.FromModel(userModel);
            return Ok(userViewModel);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] UserEntryModel userEntryModel)
        {
            if (userEntryModel.UserId == 0)
            {
                if (await _userService.IsUserNameExistsAsync(userEntryModel.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                    //return BadRequest("Username already exists");
                }

                UserModel userModel = userEntryModel.ToModel();
                UserModel result = await _userService.CreateUserAsync(userModel);

                if (result != null)
                {
                    UserViewModel? userViewModel = UserViewModel.FromModel(result);
                    return Ok(userViewModel);
                    //return CreatedAtAction("GetUserByIdAsync", new { id = userViewModel?.UserId }, userViewModel);
                }
                else
                {
                    ModelState.AddModelError("User", string.Format("No category created"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("User", string.Format("Assigning value to user id not allowed"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(int id, [FromBody] UserEntryModel userEntryModel)
        {
            if (id != userEntryModel.UserId)
            {
                ModelState.AddModelError("User", string.Format("User id mismatch"));
                return NotFound(new ValidationProblemDetails(ModelState));
            }

            if (userEntryModel.UserId > 0)
            {
                if (await _userService.IsUserNameExistsAsync(userEntryModel.UserName, userEntryModel.UserId))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                UserModel userModel = userEntryModel.ToModel();
                bool result = await _userService.UpdateUserAsync(userModel);

                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("User", string.Format("Updation failed"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("User", string.Format("User not found"));
            return NotFound(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            if (id > 0)
            {
                bool result = await _userService.DeleteUserAsync(id);
                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("User", string.Format("Couldnt delete user"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("User", string.Format("User not found"));
            return NotFound(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("GetApprovedUsers")]
        public async Task<ActionResult<List<UserViewModel>>> GetApprovedUsersAsync()
        {
            List<UserModel> users = await _userService.GetApprovedUsersAsync();
            List<UserViewModel> userViewModelList = users.Select(u => UserViewModel.FromModel(u)).ToList();
            return Ok(userViewModelList);
        }


        [HttpGet("GetArticlesByUser/{userId}")]
        public async Task<ActionResult<List<ArticleViewModel>>> GetArticlesByUserIdAsync(int userId)
        {
            List<ArticleModel> articles = await _articleService.GetArticlesByUserIdAsync(userId);
            List<ArticleViewModel> articleViewModels = articles.Select(ArticleViewModel.FromModel).ToList();
            return Ok(articleViewModels);
        }
    }
}
