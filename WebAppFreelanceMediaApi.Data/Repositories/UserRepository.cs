using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Data.DbEntities;
using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Repositories;

namespace WebAppFreelanceMediaApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MVPDbContext _context;

        public UserRepository(MVPDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetUsersListAsync()
        {
            List<UserModel> users = await _context.Users.Include(u => u.Articles)
                .Select(u => new UserModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    IsApproved = u.IsApproved,
                    Articles = u.Articles.Select(ar => ar.ToModel()).ToList()
                }).OrderBy(u => u.FirstName).ToListAsync();
            return users;
        }

        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            UserModel? user = await _context.Users.Include(u => u.Articles).Where(u => u.UserId == userId)
                .Select(u => new UserModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    IsApproved = u.IsApproved,
                    Articles = u.Articles.Select(ar => ar.ToModel()).ToList()
                }).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            else return null;
        }
        public async Task<UserModel> CreateUserAsync(UserModel userModel)
        {
            User user = new User();
            user.FromModel(userModel);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            UserModel createdUser = user.ToModel();
            return createdUser;
        }
        public async Task<bool> UpdateUserAsync(UserModel userModel)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userModel.UserId);
            if (user != null)
            {
                user.FromModel(userModel);
                _context.Users.Update(user);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }
            else return false;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }
            return false;
        }

        public async Task<bool> ApproveUserAsync(int userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsApproved = true;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> RejectUserAsync(int userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsApproved = false;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<UserModel>> GetApprovedUsersAsync()
        {
            return await _context.Users
                .Where(u => u.IsApproved)
                .Select(u => u.ToModel())
                .ToListAsync();
        }

        public async Task<List<UserModel>> GetPendingUsersAsync()
        {
            return await _context.Users
                .Where(u => !u.IsApproved)
                .Select(u => u.ToModel())
                .ToListAsync();
        }

        public async Task<UserModel?> ValidateUserAsync(string username, string password)
        {
            UserModel? user = await _context.Users
                .Where(u => u.UserName == username && u.Password == password )
                .Select(u => u.ToModel())
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> IsUserNameExistsAsync(string username, int? excludeUserId = null)
        {
            List<User> users;

            if (excludeUserId!=null)
            {
                users = await _context.Users
                    .Where(u => u.UserName == username && u.UserId != excludeUserId.Value)
                    .ToListAsync();
            }
            else
            {
                users = await _context.Users
                    .Where(u => u.UserName == username)
                    .ToListAsync();
            }
            return users.Count > 0;
        }

    }
}
