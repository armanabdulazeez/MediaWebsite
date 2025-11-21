using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Data.DbEntities;
using WebAppFreelanceMediaApi.Data.Entities;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Repositories;

namespace WebAppFreelanceMediaApi.Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly MVPDbContext _context;

        public StaffRepository(MVPDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffModel>> GetStaffListAsync()
        {
            List<StaffModel> staffs = await _context.Staffs
                .Include(s => s.Ads)
                .Select(s => new StaffModel
                {
                    StaffId = s.StaffId,
                    UserName = s.UserName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Password = s.Password,
                    Ads = s.Ads.Select(a => a.ToModel()).ToList()
                })
                .OrderBy(s => s.FirstName)
                .ToListAsync();

            return staffs;
        }

        public async Task<StaffModel?> GetStaffByIdAsync(int staffId)
        {
            StaffModel? staff = await _context.Staffs
                .Include(s => s.Ads)
                .Where(s => s.StaffId == staffId)
                .Select(s => new StaffModel
                {
                    StaffId = s.StaffId,
                    UserName = s.UserName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Password = s.Password,
                    Ads = s.Ads.Select(a => a.ToModel()).ToList()
                })
                .FirstOrDefaultAsync();

            if (staff != null)
            {
                return staff;
            }
            else return null;
        }

        public async Task<StaffModel> CreateStaffAsync(StaffModel staffModel)
        {
            Staff staff = new Staff();
            staff.FromModel(staffModel);

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            StaffModel createdStaff = staff.ToModel();
            return createdStaff;
        }

        public async Task<bool> UpdateStaffAsync(StaffModel staffModel)
        {
            Staff? staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffModel.StaffId);

            if (staff != null)
            {
                staff.FromModel(staffModel);
                _context.Staffs.Update(staff);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }

            else return false;
        }

        public async Task<bool> DeleteStaffAsync(int staffId)
        {
            Staff? staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffId);

            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }

            return false;
        }

        public async Task<StaffModel?> ValidateStaffAsync(string username, string password)
        {
            StaffModel? staff = await _context.Staffs
                .Where(s => s.UserName == username && s.Password == password)
                .Select(s => s.ToModel())
                .FirstOrDefaultAsync();

            return staff;
        }

        public async Task<bool> IsUserNameExistsAsync(string username, int? excludeStaffId = null)
        {
            List<Staff> staffs;

            if (excludeStaffId != null)
            {
                staffs = await _context.Staffs
                    .Where(s => s.UserName == username && s.StaffId != excludeStaffId.Value)
                    .ToListAsync();
            }
            else
            {
                staffs = await _context.Staffs
                    .Where(u => u.UserName == username)
                    .ToListAsync();
            }
            return staffs.Count > 0;
        }
    }
}
