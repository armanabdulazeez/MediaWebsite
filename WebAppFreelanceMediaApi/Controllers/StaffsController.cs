using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppFreelanceMediaApi.Domain.Models;
using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.EntryModels;
using WebAppFreelanceMediaApi.Services.RepServices;
using WebAppFreelanceMediaApi.ViewModels;

namespace WebAppFreelanceMediaApi.Controllers
{
    public class StaffsController : BaseApiController
    {
        private readonly IStaffService _staffService;
        private readonly IAdService _adService;

        public StaffsController(IStaffService staffService, IAdService adService)
        {
            this._staffService = staffService;
            this._adService = adService;
        }

        [HttpGet("GetStaffById/{id}")]
        public async Task<ActionResult<StaffViewModel>> GetStaffByIdAsync(int id)
        {
            StaffModel? staffModel = await _staffService.GetStaffByIdAsync(id);
            if (staffModel == null)
            {
                ModelState.AddModelError("Staff", string.Format("Staff not found"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            StaffViewModel? staffViewModel = StaffViewModel.FromModel(staffModel);
            return Ok(staffViewModel);
        }

        

        [HttpPut("UpdateStaff/{id}")]
        public async Task<ActionResult<bool>> UpdateStaff(int id, [FromBody] StaffEntryModel staffEntryModel)
        {
            if (id != staffEntryModel.StaffId)
            {
                ModelState.AddModelError("Staff", string.Format("Staff id mismatch"));
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (staffEntryModel.StaffId > 0)
            {
                if (await _staffService.IsUserNameExistsAsync(staffEntryModel.UserName, staffEntryModel.StaffId))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                StaffModel staffModel = staffEntryModel.ToModel();
                bool result = await _staffService.UpdateStaffAsync(staffModel);

                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Staff", string.Format("Updation failed"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Staff", string.Format("Staff not found"));
            return NotFound(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("DeleteStaff/{id}")]
        public async Task<ActionResult<bool>> DeleteStaff(int id)
        {
            if (id > 0)
            {
                bool result = await _staffService.DeleteStaffAsync(id);
                if (result)
                    return Ok(result);
                else
                {
                    ModelState.AddModelError("Staff", string.Format("Couldnt delete Staff"));
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            ModelState.AddModelError("Staff", string.Format("Staff not found"));
            return BadRequest(new ValidationProblemDetails(ModelState));
        }


        [HttpGet("GetAdsByStaff/{staffId}")]
        public async Task<ActionResult<List<AdViewModel>>> GetAdsByStaffIdAsync(int staffId)
        {
            List<AdModel> ads = await _adService.GetAdsByStaffIdAsync(staffId);
            List<AdViewModel> adViewModels = ads.Select(AdViewModel.FromModel).ToList();
            return Ok(adViewModels);
        }
    }
}
