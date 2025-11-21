using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.EntryModels
{
    public class ArticleEntryModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; } 
        public string Body { get; set; } 
        public string Category { get; set; } 
        public string? ImagePath { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsApproved { get; set; } 
        public int UserId { get; set; }

        public ArticleModel ToModel()
        {
            return new ArticleModel
            {
                ArticleId = this.ArticleId,
                Title = this.Title,
                Body = this.Body,
                Category = this.Category,
                ImagePath = this.ImagePath,
                PostedDate = this.PostedDate,
                IsApproved = this.IsApproved,
                UserId = this.UserId
            };
        }
    }
}
