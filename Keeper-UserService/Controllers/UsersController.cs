using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keeper_UserService.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ServiceResponse<List<Users>> response = await _userService.GetAllAsync();

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Ok(new { data = response.Data, message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: ex.Message);
            }
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                ServiceResponse<Users> response = await _userService.GetByIdAsync(id);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Ok(new { data = response.Data, message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: ex.Message);
            }
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                ServiceResponse<Users> response = await _userService.GetByEmailAsync(email);

                if (!response.IsSuccess)
                    return StatusCode(statusCode: response.Status, new { message = response.Message });

                return Ok(new { data = response.Data, message = response.Message });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: ex.Message);
            }
        }


        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] CreateUserDTO newUser)
        {
            try
            {
                ServiceResponse<Users> userServiceResponse = await _userService.CreateAsync(newUser);

                if (!userServiceResponse.IsSuccess)
                    return StatusCode(statusCode: userServiceResponse.Status, new { message = userServiceResponse.Message });

                return StatusCode(statusCode: 201, new { data = userServiceResponse.Data, message = "User was created." });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, detail: $"User service: {ex.Message}");
            }
        }
    }
}
