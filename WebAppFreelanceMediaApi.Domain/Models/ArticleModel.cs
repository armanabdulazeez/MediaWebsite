using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppFreelanceMediaApi.Domain.Models
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime PostedDate { get; set; }
        public bool IsApproved { get; set; } = false;
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
