using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Keeper_UserService.Controllers
{
    [ApiController]
    [Route("profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedProfiles([FromQuery] PagedRequestDTO<ProfileFilterDTO> pagedRequestDTO)
        {
            ServiceResponse<PagedResultDTO<ProfileDTO>> response = await _profileService.GetProfilesPagedAsync(pagedRequestDTO);
            return HandleServiceResponse(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProfileById(Guid id)
        {
            ServiceResponse<ProfileDTO?> response = await _profileService.GetByIdAsync(id);
            return HandleServiceResponse(response);
        }


        [HttpPost("batch")]
        public async Task<IActionResult> GetBatchedProfiles([FromBody] BatchedProfilesQueryDTO request)
        {
            ServiceResponse<ICollection<ProfileDTO>?> response = await _profileService.GetBatchedAsync(request);
            return HandleServiceResponse(response);
        }


        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileDTO updateProfileDTO)
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out var userId))
                return Unauthorized(new { message = "Invalid token: user ID missing or malformed." });

            ServiceResponse<ProfileDTO?> response = await _profileService.UpdateAsync(id, userId, updateProfileDTO);
            return HandleServiceResponse(response);
        }


        private IActionResult HandleServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.IsSuccess)
                return StatusCode(response.Status, new { message = response.Message });

            return Ok(new { data = response.Data, message = response.Message });
        }
    }
}
