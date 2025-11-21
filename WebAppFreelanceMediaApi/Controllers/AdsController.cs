using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;
using WebAppFreelanceMediaApi.ViewModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class AdsController : BaseApiController
    {
        private readonly IAdService _adService;

        public AdsController(IAdService adService)
        {
            this._adService = adService;
        }

        [HttpGet("GetAllAds")]
        public async Task<ActionResult<List<AdViewModel>>> GetAllAdsAsync()
        {
            List<AdModel> ads = await _adService.GetAdsListAsync();
            List<AdViewModel?> adViewModelList = new List<AdViewModel?>();

            foreach (AdModel ad in ads)
            {
                adViewModelList.Add(AdViewModel.FromModel(ad));
            }
            return Ok(adViewModelList);
        }

        [HttpGet("GetAdById/{id}")]
        public async Task<ActionResult<AdViewModel>> GetAdByIdAsync(int id)
        {
            AdModel? adModel = await _adService.GetAdByIdAsync(id);
            if (adModel == null)
            {
                ModelState.AddModelError("Ad", string.Format("Ad not found"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            AdViewModel? adViewModel = AdViewModel.FromModel(adModel);
            return Ok(adViewModel);
        }

        [HttpPost("CreateAd")]
        public async Task<ActionResult<AdViewModel>> CreateAd([FromBody] AdEntryModel adEntryModel)
        {
            if (adEntryModel.AdId == 0)
            {
                AdModel adModel = adEntryModel.ToModel();
                AdModel result = await _adService.CreateAdAsync(adModel);

                if (result != null)
                {
                    AdViewModel? adViewModel = AdViewModel.FromModel(result);
                    return Ok(adViewModel);
                    //return CreatedAtAction("GetAdByIdAsync", new { id = adViewModel?.AdId }, adViewModel);
                }
                else
                {
                    ModelState.AddModelError("Ad", string.Format("No Ad created"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("Ad", string.Format("Assigning value to Ad id not allowed"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPut("UpdateAd/{id}")]
        public async Task<ActionResult<bool>> UpdateAd(int id, [FromBody] AdEntryModel adEntryModel)
        {
            if (id != adEntryModel.AdId)
            {
                ModelState.AddModelError("Ad", string.Format("Ad id mismatch"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (adEntryModel.AdId > 0)
            {
                AdModel adModel = adEntryModel.ToModel();
                bool result = await _adService.UpdateAdAsync(adModel);

                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Ad", string.Format("Updation failed"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Ad", string.Format("Ad not found"));
            return NotFound(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("DeleteAd/{id}")]
        public async Task<ActionResult<bool>> DeleteAd(int id)
        {
            if (id > 0)
            {
                bool result = await _adService.DeleteAdAsync(id);
                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Ad", string.Format("Couldnt delete Ad"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Ad", string.Format("Ad not found"));
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("GetAdsByCategory/{category}")]
        public async Task<ActionResult<List<AdViewModel>>> GetAdsByCategoryAsync(string category)
        {
            List<AdModel> ads = await _adService.GetAdsByCategoryAsync(category);
            List<AdViewModel> adViewModels = ads.Select(AdViewModel.FromModel).ToList();
            return Ok(adViewModels);
        }
    }
}
