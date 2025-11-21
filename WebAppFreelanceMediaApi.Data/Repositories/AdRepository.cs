using Azure.Core;
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
    public class AdRepository : IAdRepository
    {
        private readonly MVPDbContext _context;

        public AdRepository(MVPDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdModel>> GetAdsListAsync()
        {
            List<AdModel> ads = await _context.Ads
                .Include(a => a.Staff)
                .Select(a => new AdModel
                {
                    AdId = a.AdId,
                    Title = a.Title,
                    Description = a.Description,
                    Category = a.Category,
                    ImagePath = a.ImagePath,
                    StaffId = a.StaffId,
                    Staff = a.Staff.ToModel()
                })
                .OrderBy(a => a.Title)
                .ToListAsync();

            return ads;
        }

        public async Task<AdModel?> GetAdByIdAsync(int adId)
        {
            AdModel? ad = await _context.Ads
                .Include(a => a.Staff)
                .Where(a => a.AdId == adId)
                .Select(a => new AdModel
                {
                    AdId = a.AdId,
                    Title = a.Title,
                    Description = a.Description,
                    Category = a.Category,
                    ImagePath = a.ImagePath,
                    StaffId = a.StaffId,
                    Staff = a.Staff.ToModel()
                })
                .FirstOrDefaultAsync();

            if (ad != null)
            {
                return ad;
            }
            else return null;
        }

        public async Task<AdModel> CreateAdAsync(AdModel adModel)
        {
            Staff? staff=await _context.Staffs.FirstOrDefaultAsync(s=>s.StaffId == adModel.StaffId);

            if (staff != null)
            {
                Ad ad = new Ad();
                ad.FromModel(adModel);

                _context.Ads.Add(ad);
                await _context.SaveChangesAsync();

                AdModel createdAd = ad.ToModel();
                return createdAd;
            }
            return null;
        }

        public async Task<bool> UpdateAdAsync(AdModel adModel)
        {
            Ad? ad = await _context.Ads.FirstOrDefaultAsync(a => a.AdId == adModel.AdId);

            if (ad != null)
            {
                Staff? staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == adModel.StaffId);
                if (staff != null)
                {
                    ad.FromModel(adModel);
                    _context.Ads.Update(ad);
                    bool result = await _context.SaveChangesAsync() > 0;
                    return result;
                }
                else return false;
            }
            else return false;
        }

        public async Task<bool> DeleteAdAsync(int adId)
        {
            Ad? ad = await _context.Ads.FirstOrDefaultAsync(a => a.AdId == adId);

            if (ad != null)
            {
                _context.Ads.Remove(ad);
                bool result = await _context.SaveChangesAsync() > 0;
                return result;
            }

            return false;
        }

        public async Task<List<AdModel>> GetAdsByStaffIdAsync(int staffId)
        {
            List<AdModel> ads = await _context.Ads
                .Where(a => a.StaffId == staffId)
                .Include(a => a.Staff)
                .Select(a => new AdModel
                {
                    AdId = a.AdId,
                    Title = a.Title,
                    Description = a.Description,
                    Category = a.Category,
                    ImagePath = a.ImagePath,
                    StaffId = a.StaffId,
                    Staff = a.Staff.ToModel()
                })
                .ToListAsync();

            return ads;
        }

        public async Task<List<AdModel>> GetAdsByCategoryAsync(string category)
        {
            return await _context.Ads
                .Where(a => a.Category == category)
                .Include(a => a.Staff)
                .Select(a => new AdModel
                {
                    AdId = a.AdId,
                    Title = a.Title,
                    Description = a.Description,
                    Category = a.Category,
                    ImagePath = a.ImagePath,
                    StaffId = a.StaffId,
                    Staff = a.Staff.ToModel()
                })
                .ToListAsync();
        }
    }
}
