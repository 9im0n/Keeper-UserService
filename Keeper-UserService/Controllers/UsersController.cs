using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_UserService.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PagedRequestDTO<UserFilterDTO> pagedRequestDTO)
        {
            ServiceResponse<PagedResultDTO<UserDTO>> response = await _userService.GetPagedAsync(pagedRequestDTO);
            return HandleServiceResponse(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            ServiceResponse<UserDTO?> response = await _userService.GetByIdAsync(id);
            return HandleServiceResponse(response);
        }

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            ServiceResponse<UserDTO?> response = await _userService.GetByEmailAsync(email);
            return HandleServiceResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            ServiceResponse<UserDTO?> response = await _userService.CreateAsync(createUserDTO);
            return HandleServiceResponse(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            ServiceResponse<UserDTO?> response = await _userService.UpdateUserAsync(id, updateUserDTO);
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
