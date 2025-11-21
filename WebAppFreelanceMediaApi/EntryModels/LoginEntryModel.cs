using System.ComponentModel.DataAnnotations;

namespace WebAppFreelanceMediaApi.EntryModels
{
    public class LoginEntryModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
