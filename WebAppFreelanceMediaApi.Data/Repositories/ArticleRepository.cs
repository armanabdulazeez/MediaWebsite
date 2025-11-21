using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Data.DbEntities;
using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Repositories;

namespace WebAppFreelanceMediaApi.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MVPDbContext _context;

        public ArticleRepository(MVPDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticleModel>> GetArticlesListAsync()
        {
            List<ArticleModel> articles = await _context.Articles
                .Include(a => a.User)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .OrderByDescending(a => a.PostedDate)
                .ToListAsync();

            return articles;
        }

        public async Task<ArticleModel?> GetArticleByIdAsync(int articleId)
        {
            ArticleModel? article = await _context.Articles
                .Include(a => a.User)
                .Where(a => a.ArticleId == articleId)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .FirstOrDefaultAsync();

            if (article != null)
            {
                return article;
            }
            else return null;
        }

        public async Task<ArticleModel> CreateArticleAsync(ArticleModel articleModel)
        {
            User? user=await _context.Users.FirstOrDefaultAsync(u=>u.UserId==articleModel.UserId);

            if (user != null)
            {
                Article article = new Article();
                article.FromModel(articleModel);

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                ArticleModel createdArticle = article.ToModel();
                return createdArticle;
            }
            return null;
        }

        public async Task<bool> UpdateArticleAsync(ArticleModel articleModel)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleModel.ArticleId);

            if (article != null)
            {
                User? user = await _context.Users.FirstOrDefaultAsync(a => a.UserId == articleModel.UserId);
                if (user != null)
                {
                    article.FromModel(articleModel);
                    _context.Articles.Update(article);
                    bool result = await _context.SaveChangesAsync() > 0;
                    return result;
                }
                else return false;
            }
            else return false;
        }

        public async Task<bool> DeleteArticleAsync(int articleId)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleId);

            if (article != null)
            {
                _context.Articles.Remove(article);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }

            return false;
        }

        public async Task<List<ArticleModel>> GetApprovedArticlesAsync()
        {
            return await _context.Articles
                .Where(a => a.IsApproved)
                .OrderByDescending(a => a.PostedDate)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .ToListAsync();
        }

        public async Task<List<ArticleModel>> GetPendingArticlesAsync()
        {
            return await _context.Articles
                .Where(a => !a.IsApproved)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .ToListAsync();
        }

        public async Task<bool> ApproveArticleAsync(int articleId)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleId);

            if (article != null)
            {
                article.IsApproved = true;
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RejectArticleAsync(int articleId)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleId);

            if (article != null)
            {
                article.IsApproved = false;
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<List<ArticleModel>> GetArticlesByCategoryAsync(string category)
        {
            return await _context.Articles
                .Where(a => a.Category == category && a.IsApproved)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .ToListAsync();
        }

        public async Task<List<ArticleModel>> GetArticlesByUserIdAsync(int userId)
        {
            return await _context.Articles
                .Where(a => a.UserId == userId)
                .Select(a => new ArticleModel
                {
                    ArticleId = a.ArticleId,
                    Title = a.Title,
                    Body = a.Body,
                    Category = a.Category,
                    IsApproved = a.IsApproved,
                    ImagePath = a.ImagePath,
                    PostedDate = a.PostedDate,
                    UserId = a.UserId,
                    User = a.User.ToModel()
                })
                .ToListAsync();
        }
    }
}
