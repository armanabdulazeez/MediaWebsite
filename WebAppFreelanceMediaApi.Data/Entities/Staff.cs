using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Data.Entities
{
    public class Staff
    {
        public Staff()
        {
            Ads = new List<Ad>();
        }
        public int StaffId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public ICollection<Ad>? Ads { get; set; }

        public Staff FromModel(StaffModel staffModel)
        {
            if(staffModel != null)
            {
                this.StaffId = staffModel.StaffId;
                this.UserName = staffModel.UserName;
                this.FirstName = staffModel.FirstName;
                this.LastName = staffModel.LastName;
                this.Email = staffModel.Email;
                this.PhoneNumber = staffModel.PhoneNumber;
                this.Password = staffModel.Password;
            }
            return this;
        }

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
