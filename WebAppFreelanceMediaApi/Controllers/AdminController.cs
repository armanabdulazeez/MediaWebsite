using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;
using WebAppFreelanceMediaApi.Services.RepServices;
using WebAppFreelanceMediaApi.ViewModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IArticleService _articleService;
        private readonly IAdService _adService;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IConfiguration _configuration;

        public AdminController(IArticleService articleService,IAdService adService ,IUserService userService, IStaffService staffService, IConfiguration configuration)
        {
            this._articleService = articleService;
            this._adService = adService;
            this._userService = userService;
            this._staffService = staffService;
            this._configuration = configuration;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserViewModel>>> GetAllUsersAsync()
        {
            List<UserModel> users = await _userService.GetUsersListAsync();
            List<UserViewModel?> userViewModelList = new List<UserViewModel?>();

            foreach (UserModel user in users)
            {
                userViewModelList.Add(UserViewModel.FromModel(user));
            }
            return Ok(userViewModelList);
        }

        [HttpGet("GetApprovedUsers")]
        public async Task<ActionResult<List<UserViewModel>>> GetApprovedUsersAsync()
        {
            List<UserModel> users = await _userService.GetApprovedUsersAsync();
            List<UserViewModel> userViewModelList = users.Select(u => UserViewModel.FromModel(u)).ToList();
            return Ok(userViewModelList);
        }

        [HttpGet("GetPendingUsers")]
        public async Task<ActionResult<List<UserViewModel>>> GetPendingUsersAsync()
        {
            List<UserModel> users = await _userService.GetPendingUsersAsync();
            List<UserViewModel> userViewModelList = users.Select(u => UserViewModel.FromModel(u)).ToList();
            return Ok(userViewModelList);
        }


        [HttpPost("ApproveUser/{id}")]
        public async Task<ActionResult<bool>> ApproveUser(int id)
        {
            bool result = await _userService.ApproveUserAsync(id);
            if (result)
                return Ok(result);
            ModelState.AddModelError("User", "Could not approve user");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("RejectUser/{id}")]
        public async Task<ActionResult<bool>> RejectUser(int id)
        {
            bool result = await _userService.RejectUserAsync(id);
            if (result)
                return Ok(result);
            ModelState.AddModelError("User", "Could not reject user");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("GetAllStaffs")]
        public async Task<ActionResult<List<StaffViewModel>>> GetAllStaffsAsync()
        {
            List<StaffModel> staffs = await _staffService.GetStaffListAsync();
            List<StaffViewModel?> staffViewModelList = new List<StaffViewModel?>();

            foreach (StaffModel staff in staffs)
            {
                staffViewModelList.Add(StaffViewModel.FromModel(staff));
            }
            return Ok(staffViewModelList);
        }

        [HttpPost("CreateStaff")]
        public async Task<ActionResult<StaffViewModel>> CreateStaff([FromBody] StaffEntryModel staffEntryModel)
        {
            if (staffEntryModel.StaffId == 0)
            {
                if (await _staffService.IsUserNameExistsAsync(staffEntryModel.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                StaffModel staffModel = staffEntryModel.ToModel();
                StaffModel result = await _staffService.CreateStaffAsync(staffModel);

                if (result != null)
                {
                    StaffViewModel? staffViewModel = StaffViewModel.FromModel(result);
                    return Ok(staffViewModel);
                    //return CreatedAtAction("GetStaffByIdAsync", new { id = staffViewModel?.StaffId }, staffViewModel);
                }
                else
                {
                    ModelState.AddModelError("Staff", string.Format("No staff created"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("Staff", string.Format("Assigning value to staff id not allowed"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPost("ApproveArticle/{id}")]
        public async Task<ActionResult<bool>> ApproveArticle(int id)
        {
            bool result = await _articleService.ApproveArticleAsync(id);
            if (result)
                return Ok(result);

            ModelState.AddModelError("Article", "Could not approve article");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("RejectArticle/{id}")]
        public async Task<ActionResult<bool>> RejectArticle(int id)
        {
            bool result = await _articleService.RejectArticleAsync(id);
            if (result)
                return Ok(result);

            ModelState.AddModelError("Article", "Could not reject article");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("GetApprovedArticles")]
        public async Task<ActionResult<List<ArticleViewModel>>> GetApprovedArticlesAsync()
        {
            List<ArticleModel> articles = await _articleService.GetApprovedArticlesAsync();
            List<ArticleViewModel> articleViewModels = articles.Select(ArticleViewModel.FromModel).ToList();
            return Ok(articleViewModels);
        }

        [HttpGet("GetPendingArticles")]
        public async Task<ActionResult<List<ArticleViewModel>>> GetPendingArticlesAsync()
        {
            List<ArticleModel> articles = await _articleService.GetPendingArticlesAsync();
            List<ArticleViewModel> articleViewModels = articles.Select(ArticleViewModel.FromModel).ToList();
            return Ok(articleViewModels);
        }

    }
}
