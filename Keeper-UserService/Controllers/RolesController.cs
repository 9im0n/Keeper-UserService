using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_UserService.Controllers
{
    [ApiController]
    [Route("roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _rolesService.GetAllRolesAsync();
            return HandleServiceResponse(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var response = await _rolesService.GetByIdAsync(id);
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
