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
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;

        public AdService(IAdRepository adRepository)
        {
            this._adRepository = adRepository;
        }

        public async Task<AdModel> CreateAdAsync(AdModel adModel)
            => await _adRepository.CreateAdAsync(adModel);

        public async Task<bool> DeleteAdAsync(int adId)
            => await _adRepository.DeleteAdAsync(adId);

        public async Task<AdModel?> GetAdByIdAsync(int adId)
            => await _adRepository.GetAdByIdAsync(adId);

        public async Task<List<AdModel>> GetAdsByStaffIdAsync(int staffId)
            => await _adRepository.GetAdsByStaffIdAsync(staffId);

        public async Task<List<AdModel>> GetAdsListAsync()
            => await _adRepository.GetAdsListAsync();

        public async Task<bool> UpdateAdAsync(AdModel adModel)
            => await _adRepository.UpdateAdAsync(adModel);

        public async Task<List<AdModel>> GetAdsByCategoryAsync(string category)
           => await _adRepository.GetAdsByCategoryAsync(category);
    }
}
