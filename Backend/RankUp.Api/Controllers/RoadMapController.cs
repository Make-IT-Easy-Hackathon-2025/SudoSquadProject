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
    [Route("api/roadmaps")]
    public class RoadMapController : ControllerBase
    {
        private readonly IRoadMapService _roadMapService;

        private readonly IMemoryService _memoryService;

        private readonly IMapper _mapper;

        private readonly IOptions<JwtSettings> _jwtSettings;

        public RoadMapController(IRoadMapService roadMapService, IMemoryService memoryService, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _roadMapService = roadMapService;
            _memoryService = memoryService;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("prompt")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoadMapDTO))]
        [Authorize]
        public async Task<IActionResult> GenerateRoadMapAsync([FromBody] PromptInputDTO promptInput, CancellationToken cancellationToken = default)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if (userId == null)
            {
                return BadRequest();
            }

            var roadmap = await _roadMapService.GenerateRoadMapAsync(promptInput.Keyword, cancellationToken);


            var result = await _roadMapService.CreateRoadMapAsync(_mapper.Map<RoadMap>(roadmap), cancellationToken);

            await _memoryService.GenerateMemoryFromRoadMapAsync(userId.Value, result.Id, cancellationToken);

            return Ok(_mapper.Map<RoadMapDTO>(result));
        }

        [HttpPost]
        [Route("complate-items")]
        [Authorize]
        public async Task<IActionResult> ComplateRoadMapItems([FromBody] CompletedMapItems completedMapItems, CancellationToken cancellationToken)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if (userId == null)
            {
                return BadRequest();
            }

            var result = await _roadMapService.ComplateRoadItmesAsync(completedMapItems.ComplatedItemIds, userId.Value, cancellationToken);

            return Ok(result);

        }

        [HttpGet]
        [Route("{id}/replay")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoadMapRepayDTO))]
        public async Task<IActionResult> GetRoadMapReplay([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if (userId == null)
            {
                return BadRequest();
            }

            return Ok(await _roadMapService.GetRoadMapReplayAsync(userId.Value, id, cancellationToken));


        }
    }
}
