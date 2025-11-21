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
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            this._staffRepository = staffRepository;
        }

        public async Task<StaffModel> CreateStaffAsync(StaffModel staffModel)
            => await _staffRepository.CreateStaffAsync(staffModel);

        public async Task<bool> DeleteStaffAsync(int staffId)
            => await _staffRepository.DeleteStaffAsync(staffId);

        public async Task<StaffModel?> GetStaffByIdAsync(int staffId)
            => await _staffRepository.GetStaffByIdAsync(staffId);

        public async Task<List<StaffModel>> GetStaffListAsync()
            => await _staffRepository.GetStaffListAsync();

        public async Task<bool> UpdateStaffAsync(StaffModel staffModel)
            => await _staffRepository.UpdateStaffAsync(staffModel);

        public async Task<StaffModel?> ValidateStaffAsync(string username, string password)
            => await _staffRepository.ValidateStaffAsync(username, password);

        public async Task<bool> IsUserNameExistsAsync(string username, int? excludeStaffId = null)
            => await _staffRepository.IsUserNameExistsAsync(username, excludeStaffId);
    }
}
