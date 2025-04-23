using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_UserService.Controllers
{
    [Route("profiles")]
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                ServiceResponse<Profiles?> response = await _profileService.GetByIdAsync(id);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = $"User Service: {response.Message}" });

                return Ok(new { data = response.Data, message = $"User Service: {response.Message}" });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"User Service: {ex.Message}. {ex.StackTrace}");
            }
        }

        [Authorize]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfileDTO updateProfileDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ServiceResponse<Profiles?> response = await _profileService.UpdateAsync(new Guid(User.FindFirst("UserId")?.Value), updateProfileDTO);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = $"User Service: {response.Message}" });

                return Ok(new { data = response.Data, message = $"User Service: {response.Message}" });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"User Service: {ex.Message}. {ex.StackTrace}");
            }
        }
    }
}
