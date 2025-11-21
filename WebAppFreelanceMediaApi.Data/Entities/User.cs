using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Data.Entities
{
    public class User
    {
        public User()
        {
            Articles = new List<Article>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName {  get; set; }=string.Empty;
        public string LastName {  get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;

        public ICollection<Article>? Articles { get; set; }

        public User FromModel(UserModel userModel)
        {
            if(userModel != null)
            {
                this.UserId = userModel.UserId;
                this.UserName = userModel.UserName;
                this.FirstName = userModel.FirstName;
                this.LastName = userModel.LastName;
                this.Email = userModel.Email;
                this.Password = userModel.Password;
                this.IsApproved = userModel.IsApproved;
            }
            return this;
        }

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
