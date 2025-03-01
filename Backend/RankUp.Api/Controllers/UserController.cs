using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RankUpp.Api.Configurations;
using RankUpp.Api.Helpers;
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

        private readonly IOptions<JwtSettings> _jwtSettings;

        private readonly IStatisticsService _statisticsService;

        public UserController(IUserService userService, IOptions<JwtSettings> jwtSettings, IStatisticsService statisticsService)
        {
            _userService = userService;

            _jwtSettings = jwtSettings;

            _statisticsService = statisticsService;
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

        [HttpGet]
        [Route("statistcs")]
        [Authorize]
        public async Task<IActionResult> GetStatistcsFromUser()
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if (userId == null)
            {
                return BadRequest();
            }

            return Ok(await _statisticsService.CalculateUserStatistics(userId.Value));
        }


    }
}
