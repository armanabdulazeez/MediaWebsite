using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Domain.Repositories
{
    public interface IStaffRepository
    {
        Task<StaffModel> CreateStaffAsync(StaffModel staffModel);
        Task<List<StaffModel>> GetStaffListAsync();
        Task<StaffModel?> GetStaffByIdAsync(int staffId);
        Task<bool> UpdateStaffAsync(StaffModel staffModel);
        Task<bool> DeleteStaffAsync(int staffId);

        Task<StaffModel?> ValidateStaffAsync(string username, string password);
        Task<bool> IsUserNameExistsAsync(string username, int? excludeStaffId = null);
    }
}
