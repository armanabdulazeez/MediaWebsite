using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Data.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsApproved { get; set; } = false;
        public int UserId { get; set; }
        public User User { get; set; }

        public Article FromModel(ArticleModel articleModel)
        {
            if (articleModel != null)
            {
                this.ArticleId = articleModel.ArticleId;
                this.Title = articleModel.Title;
                this.Body = articleModel.Body;
                this.Category = articleModel.Category;
                this.ImagePath = articleModel.ImagePath;
                this.PostedDate = articleModel.PostedDate;
                this.IsApproved = articleModel.IsApproved;
                this.UserId = articleModel.UserId;
            }
            return this;
        }

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
