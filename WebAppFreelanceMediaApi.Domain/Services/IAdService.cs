using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Domain.Services
{
    public interface IAdService
    {
        Task<AdModel> CreateAdAsync(AdModel adModel);
        Task<List<AdModel>> GetAdsListAsync();
        Task<AdModel?> GetAdByIdAsync(int adId);
        Task<bool> UpdateAdAsync(AdModel adModel);
        Task<bool> DeleteAdAsync(int adId);

        Task<List<AdModel>> GetAdsByStaffIdAsync(int staffId);
        Task<List<AdModel>> GetAdsByCategoryAsync(string category);
    }
}
