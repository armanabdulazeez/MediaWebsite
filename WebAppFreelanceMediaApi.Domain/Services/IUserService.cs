using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Domain.Services
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync(UserModel userModel);
        Task<List<UserModel>> GetUsersListAsync();
        Task<UserModel?> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(UserModel userModel);
        Task<bool> DeleteUserAsync(int userId);

        Task<bool> ApproveUserAsync(int userId);
        Task<bool> RejectUserAsync(int userId);

        Task<List<UserModel>> GetPendingUsersAsync();
        Task<List<UserModel>> GetApprovedUsersAsync();

        Task<UserModel?> ValidateUserAsync(string username, string password);
        Task<bool> IsUserNameExistsAsync(string username, int? excludeUserId = null);
    }
}
