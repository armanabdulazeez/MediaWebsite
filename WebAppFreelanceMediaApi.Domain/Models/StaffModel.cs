using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppFreelanceMediaApi.Domain.Models
{
    public class StaffModel
    {
        public int StaffId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public ICollection<AdModel>? Ads { get; set; }
    }
}
