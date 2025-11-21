using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.ViewModels
{
    public class StaffViewModel
    {
        public int StaffId { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public long PhoneNumber { get; set; }
        public string Password { get; set; } 
        public ICollection<AdViewModel>? Ads { get; set; }

        public static StaffViewModel? FromModel(StaffModel staffModel)
        {
            if (staffModel != null)
            {
                List<AdViewModel?>? adViewModelList = new List<AdViewModel?>();
                adViewModelList=staffModel.Ads?.Select(am=>AdViewModel.FromModel(am)).ToList();
                return new StaffViewModel
                {
                    StaffId = staffModel.StaffId,
                    UserName = staffModel.UserName,
                    FirstName = staffModel.FirstName,
                    LastName = staffModel.LastName,
                    Email = staffModel.Email,
                    PhoneNumber = staffModel.PhoneNumber,
                    Password = staffModel.Password,
                    Ads = adViewModelList
                };
            }
            return null;
        }
    }
}
