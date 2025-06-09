using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_UserService.Controllers
{
    [ApiController]
    [Route("permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            ServiceResponse<List<PermissionDTO>> response = await _permissionsService.GetAllPermissionsAsync();
            return HandleServiceResponse(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPermissionsById(Guid id)
        {
            ServiceResponse<PermissionDTO?> response = await _permissionsService.GetByIdAsync(id);
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
