using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public bool IsApproved { get; set; } 

        public ICollection<ArticleViewModel>? Articles { get; set; }

        public static UserViewModel? FromModel(UserModel userModel)
        {
            if (userModel != null)
            {
                List<ArticleViewModel?>? articleViewModelList = new List<ArticleViewModel?>();
                articleViewModelList = userModel.Articles?.Select(am => ArticleViewModel.FromModel(am)).ToList();
                return new UserViewModel
                {
                    UserId = userModel.UserId,
                    UserName = userModel.UserName,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email,
                    Password = userModel.Password,
                    IsApproved = userModel.IsApproved,
                    Articles = articleViewModelList
                };
            }
            return null;
        }
    }
}
