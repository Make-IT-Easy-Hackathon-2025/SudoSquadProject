using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RankUpp.Api.Configurations;
using RankUpp.Api.Helpers;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;

namespace RankUpp.Api.Controllers
{
    [ApiController]
    [Route("memories")]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryService _memoryService;

        private IOptions<JwtSettings> _jwtSettings;

        private readonly IMapper _mapper;

        public MemoryController(IMemoryService memoryService, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _memoryService = memoryService;

            _mapper = mapper;

            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMemoryDTO))]
        public async Task<IActionResult> CreateMemory([FromBody] CreateMemoryRequestDTO createMemoryRequestDTO, CancellationToken cancellationToken)
        {
            var memory = _mapper.Map<UserMemory>(createMemoryRequestDTO);

            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var UserId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if(UserId == null)
            {
                return BadRequest();
            }

            memory.UserId = UserId.Value;

            var result = await _memoryService.CreateMemoryAsync(memory, cancellationToken);

            return Ok(_mapper.Map<UserMemoryDTO>(result));

        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMemoryDTO))]
        public async Task<IActionResult> GetMemories([FromQuery] int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if(userId == null)
            {
                return BadRequest();
            }

            var result = await _memoryService.GetUserMemoriesAsync(userId.Value, pageNumber, pageSize, cancellationToken);

            return Ok(result.ConvertAll(_mapper.Map<UserMemoryDTO>));
        }
    }
}
