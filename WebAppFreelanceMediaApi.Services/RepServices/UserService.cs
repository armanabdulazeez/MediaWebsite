using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Repositories;
using WebAppFreelanceMediaApi.Domain.Services;

namespace WebAppFreelanceMediaApi.Services.RepServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<UserModel> CreateUserAsync(UserModel userModel)
            => await _userRepository.CreateUserAsync(userModel);

        public async Task<bool> DeleteUserAsync(int userId)
            => await _userRepository.DeleteUserAsync(userId);

        public async Task<List<UserModel>> GetUsersListAsync()
            => await _userRepository.GetUsersListAsync();

        public async Task<UserModel?> GetUserByIdAsync(int userId)
            => await _userRepository.GetUserByIdAsync(userId);

        public async Task<bool> UpdateUserAsync(UserModel userModel)
            => await _userRepository.UpdateUserAsync(userModel);

        public async Task<bool> ApproveUserAsync(int userId)
            => await _userRepository.ApproveUserAsync(userId);

        public async Task<bool> RejectUserAsync(int userId)
            => await _userRepository.RejectUserAsync(userId);

        public async Task<List<UserModel>> GetApprovedUsersAsync()
            => await _userRepository.GetApprovedUsersAsync();

        public async Task<List<UserModel>> GetPendingUsersAsync()
            => await _userRepository.GetPendingUsersAsync();

        public async Task<UserModel?> ValidateUserAsync(string username, string password)
            =>await _userRepository.ValidateUserAsync(username, password);

        public async Task<bool> IsUserNameExistsAsync(string username, int? excludeUserId = null)
            =>await _userRepository.IsUserNameExistsAsync(username, excludeUserId);
    }
}
