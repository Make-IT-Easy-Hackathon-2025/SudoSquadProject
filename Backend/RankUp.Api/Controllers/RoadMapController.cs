using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.Models;

namespace RankUpp.Api.Controllers
{
    [ApiController]
    [Route("api/roadmaps")]
    public class RoadMapController : ControllerBase
    {
        private readonly IRoadMapService _roadMapService;

        private readonly IMapper _mapper;

        public RoadMapController(IRoadMapService roadMapService, IMapper mapper)
        {
            _roadMapService = roadMapService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("prompt")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoadMapDTO))]
        [Authorize]
        public async Task<IActionResult> GenerateRoadMapAsync([FromBody] PromptInputDTO promptInput, CancellationToken cancellationToken)
        {
            var roadmap = await _roadMapService.GenerateRoadMapAsync(promptInput.Keyword, cancellationToken);


            var result = await _roadMapService.CreateRoadMapAsync(_mapper.Map<RoadMap>(roadmap), cancellationToken);

            return Ok(_mapper.Map<RoadMapDTO>(result));
        }
    }
}
