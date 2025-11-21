using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.EntryModels
{
    public class UserEntryModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public bool IsApproved { get; set; } 

        public UserModel ToModel()
        {
            return new UserModel()
            {
                UserId = this.UserId,
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                IsApproved = this.IsApproved
            };
        }
    }
}
