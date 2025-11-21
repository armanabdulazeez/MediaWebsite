using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppFreelanceMediaApi.Domain.Models
{
    public class AdModel
    {
        public int AdId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public int StaffId { get; set; }
        public StaffModel Staff { get; set; }
    }
}
