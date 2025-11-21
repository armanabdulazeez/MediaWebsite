using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Repositories;
using WebAppFreelanceMediaApi.Domain.Services;

namespace WebAppFreelanceMediaApi.Services.RepServices
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<bool> ApproveArticleAsync(int articleId)
            => await _articleRepository.ApproveArticleAsync(articleId);

        public async Task<ArticleModel> CreateArticleAsync(ArticleModel articleModel)
            => await _articleRepository.CreateArticleAsync(articleModel);

        public async Task<bool> DeleteArticleAsync(int articleId)
            => await _articleRepository.DeleteArticleAsync(articleId);

        public async Task<List<ArticleModel>> GetApprovedArticlesAsync()
            => await _articleRepository.GetApprovedArticlesAsync();

        public async Task<ArticleModel?> GetArticleByIdAsync(int articleId)
            => await _articleRepository.GetArticleByIdAsync(articleId);

        public async Task<List<ArticleModel>> GetArticlesByCategoryAsync(string category)
            => await _articleRepository.GetArticlesByCategoryAsync(category);

        public async Task<List<ArticleModel>> GetArticlesByUserIdAsync(int userId)
            => await _articleRepository.GetArticlesByUserIdAsync(userId);

        public async Task<List<ArticleModel>> GetArticlesListAsync()
            => await _articleRepository.GetArticlesListAsync();

        public async Task<List<ArticleModel>> GetPendingArticlesAsync()
            => await _articleRepository.GetPendingArticlesAsync();

        public async Task<bool> RejectArticleAsync(int articleId)
            => await _articleRepository.RejectArticleAsync(articleId);

        public async Task<bool> UpdateArticleAsync(ArticleModel articleModel)
            => await _articleRepository.UpdateArticleAsync(articleModel);
    }
}
