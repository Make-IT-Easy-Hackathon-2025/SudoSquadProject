using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;

namespace RankUpp.Api.Controllers
{
    [ApiController]
    [Route("api/quizes")]
    public class QuizController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizDTO))]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizDTO createQuizDTO, CancellationToken cancellation = default)
        {
            var quiz = _mapper.Map<Quiz>(createQuizDTO);

            var result = await _quizService.CreateQuizAsync(quiz, cancellation);

            return Ok(_mapper.Map<QuizDTO>(result));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizDTO))]
        public async Task<IActionResult> GetQuizById([FromRoute] int id, CancellationToken cancellation = default)
        {
            var result = await _quizService.GetQuizByIdAsync(id, cancellation);

            return Ok(_mapper.Map<QuizDTO>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuiz([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null, CancellationToken cancellation = default)
        {
            var result = await _quizService.GetAllQuizsAsync(pageNumber, pageSize, cancellation);

            return Ok(result.ConvertAll(_mapper.Map<QuizDTO>));
        }
    }
}
