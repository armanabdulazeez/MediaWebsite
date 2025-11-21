using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.ViewModels
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; } 
        public string Body { get; set; } 
        public string Category { get; set; } 
        public string? ImagePath { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsApproved { get; set; } 
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }

        public static ArticleViewModel? FromModel(ArticleModel articleModel)
        {
            if (articleModel != null)
            {
                return new ArticleViewModel
                {
                    ArticleId = articleModel.ArticleId,
                    Title = articleModel.Title,
                    Body = articleModel.Body,
                    Category = articleModel.Category,
                    ImagePath = articleModel.ImagePath,
                    PostedDate = articleModel.PostedDate,
                    IsApproved = articleModel.IsApproved,
                    UserId = articleModel.UserId,
                    User = UserViewModel.FromModel(articleModel.User)
                };
            }
            return null;
        }
    }
}
