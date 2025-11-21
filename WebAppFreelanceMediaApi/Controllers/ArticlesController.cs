using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;
using WebAppFreelanceMediaApi.ViewModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class ArticlesController : BaseApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        [HttpGet("GetAllArticles")]
        public async Task<ActionResult<List<ArticleViewModel>>> GetAllArticlesAsync()
        {
            List<ArticleModel> articles = await _articleService.GetArticlesListAsync();
            List<ArticleViewModel?> articleViewModelList = new List<ArticleViewModel?>();

            foreach (ArticleModel article in articles)
            {
                articleViewModelList.Add(ArticleViewModel.FromModel(article));
            }
            return Ok(articleViewModelList);
        }

        [HttpGet("GetArticleById/{id}")]
        public async Task<ActionResult<ArticleViewModel>> GetArticleByIdAsync(int id)
        {
            ArticleModel? articleModel = await _articleService.GetArticleByIdAsync(id);
            if (articleModel == null)
            {
                ModelState.AddModelError("Article", string.Format("Article not found"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            ArticleViewModel? articleViewModel = ArticleViewModel.FromModel(articleModel);
            return Ok(articleViewModel);
        }

        [HttpPost("CreateArticle")]
        public async Task<ActionResult<ArticleViewModel>> CreateArticle([FromBody] ArticleEntryModel articleEntryModel)
        {
            if (articleEntryModel.ArticleId == 0)
            {
                ArticleModel articleModel = articleEntryModel.ToModel();
                ArticleModel result = await _articleService.CreateArticleAsync(articleModel);

                if (result != null)
                {
                    ArticleViewModel? articleViewModel = ArticleViewModel.FromModel(result);
                    return Ok(articleViewModel);
                    //return CreatedAtAction("GetArticleByIdAsync", new { id = articleViewModel?.ArticleId }, articleViewModel);
                }
                else
                {
                    ModelState.AddModelError("Article", string.Format("No article created"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("Article", string.Format("Assigning value to Articel id not allowed"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPut("UpdateArticle/{id}")]
        public async Task<ActionResult<bool>> UpdateArticle(int id, [FromBody] ArticleEntryModel articleEntryModel)
        {
            if (id != articleEntryModel.ArticleId)
            {
                ModelState.AddModelError("Article", string.Format("Article id mismatch"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (articleEntryModel.ArticleId > 0)
            {
                ArticleModel articleModel = articleEntryModel.ToModel();
                bool result = await _articleService.UpdateArticleAsync(articleModel);

                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Article", string.Format("Updation failed"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Article", string.Format("Article not found"));
            return NotFound(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("DeleteArticle/{id}")]
        public async Task<ActionResult<bool>> DeleteArticle(int id)
        {
            if (id > 0)
            {
                bool result = await _articleService.DeleteArticleAsync(id);
                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Article", string.Format("Couldnt delete Article"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Article", string.Format("Article not found"));
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("GetArticlesByCategory/{category}")]
        public async Task<ActionResult<List<ArticleViewModel>>> GetArticlesByCategoryAsync(string category)
        {
            List<ArticleModel> articles = await _articleService.GetArticlesByCategoryAsync(category);
            List<ArticleViewModel> articleViewModels = articles.Select(ArticleViewModel.FromModel).ToList();
            return Ok(articleViewModels);
        }

    }
}
