using Microsoft.AspNetCore.Mvc;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Output;

namespace RankUpp.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequestDTO registrationRequestDTO, CancellationToken cancellationToken)
        {
            return Ok(await _userService.CreateUserAsync(registrationRequestDTO, cancellationToken));
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO, CancellationToken cancellationToken)
        {
            return Ok(await _userService.LoginAsync(loginRequestDTO, cancellationToken));
        }


    }
}
