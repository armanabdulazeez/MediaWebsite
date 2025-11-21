using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Domain.Services
{
    public interface IArticleService
    {
        Task<ArticleModel> CreateArticleAsync(ArticleModel articleModel);
        Task<List<ArticleModel>> GetArticlesListAsync();
        Task<ArticleModel?> GetArticleByIdAsync(int articleId);
        Task<bool> UpdateArticleAsync(ArticleModel articleModel);
        Task<bool> DeleteArticleAsync(int articleId);

        Task<bool> ApproveArticleAsync(int articleId);
        Task<bool> RejectArticleAsync(int articleId);

        Task<List<ArticleModel>> GetPendingArticlesAsync();
        Task<List<ArticleModel>> GetApprovedArticlesAsync();
        Task<List<ArticleModel>> GetArticlesByUserIdAsync(int userId);
        Task<List<ArticleModel>> GetArticlesByCategoryAsync(string category);
    }
}
