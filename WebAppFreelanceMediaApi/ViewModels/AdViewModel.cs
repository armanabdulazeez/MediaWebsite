using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.ViewModels
{
    public class AdViewModel
    {
        public int AdId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public string Category { get; set; } 
        public string? ImagePath { get; set; }
        public int StaffId { get; set; }
        public StaffViewModel? Staff { get; set; }

        public static AdViewModel? FromModel(AdModel adModel)
        {
            if (adModel != null)
            {
                return new AdViewModel 
                {
                    AdId = adModel.AdId,
                    Title = adModel.Title,
                    Description = adModel.Description,
                    Category = adModel.Category,
                    ImagePath = adModel.ImagePath,
                    StaffId = adModel.StaffId,
                    Staff = StaffViewModel.FromModel(adModel.Staff)
                };
            }
            return null;
        }
    }
}
