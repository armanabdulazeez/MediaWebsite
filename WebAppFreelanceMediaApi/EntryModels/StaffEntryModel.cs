using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.EntryModels
{
    public class StaffEntryModel
    {
        public int StaffId { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public long PhoneNumber { get; set; }
        public string Password { get; set; } 

        public StaffModel ToModel()
        {
            return new StaffModel()
            {
                StaffId = this.StaffId,
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Password = this.Password
            };
        }
    }
}
