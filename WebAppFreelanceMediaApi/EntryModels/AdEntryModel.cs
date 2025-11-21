using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.EntryModels
{
    public class AdEntryModel
    {
        public int AdId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public string Category { get; set; } 
        public string? ImagePath { get; set; }
        public int StaffId { get; set; }
        public AdModel ToModel()
        {
            return new AdModel
            {
                AdId = this.AdId,
                Title = this.Title,
                Description = this.Description,
                Category = this.Category,
                ImagePath = this.ImagePath,
                StaffId = this.StaffId
            };
        }
    }
}
