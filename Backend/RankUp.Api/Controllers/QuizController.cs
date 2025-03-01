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
using System.Threading;

namespace RankUpp.Api.Controllers
{
    [ApiController]
    [Route("api/quizes")]
    public class QuizController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IQuizService _quizService;

        private readonly IOptions<JwtSettings> _jwtSettings;

        public QuizController(IQuizService quizService, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _quizService = quizService;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QuizDTO>))]
        public async Task<IActionResult> GetAllQuiz([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null, CancellationToken cancellation = default)
        {
            var result = await _quizService.GetAllQuizsAsync(pageNumber, pageSize, cancellation);

            return Ok(result.ConvertAll(_mapper.Map<QuizDTO>));
        }

        [HttpPost]
        [Route("{id}/answer")]
        [Authorize]
        public async Task<IActionResult> SubmitAnswer([FromRoute] int id, [FromBody ]QuizAnswersDTO quizAnswers, CancellationToken cancellation = default)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if(userId == null)
            {
                return BadRequest();
            }

            var attempts = quizAnswers.SelectedOptions.Select(x => new QuizAttempt { QuizOptionId = x, UserId = userId.Value, QuizId = id }).ToList();

            var result = await _quizService.AddQuizAttemptsAsync(attempts, cancellation);



            var score = await _quizService.EvaluateQuizAsync(id, userId.Value, cancellation);


            return Ok(new EvaluateQuizResult { Right = score.Item1, Wrong = score.Item1});
        }

        [HttpGet]
        [Route("{id}/replay")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizReplayDTO))]
        public async Task<IActionResult> GetQuizReplayById([FromRoute] int id, CancellationToken cancellation = default)
        {
            var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

            var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

            if (userId == null)
            {
                return BadRequest();
            }

            return Ok(await _quizService.GetQuizReplayByIdAsync(id, userId.Value, cancellation));
        }

        
        [HttpPost]
        [Route("prompt")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizDTO))]
        public async Task<IActionResult> GetQuizByPrompt([FromBody] PromptInputDTO promptInput, CancellationToken cancellation = default)
        {
            if(promptInput.UseAi == false)
            {
                var token = RequestProcessingHelper.GetAuthTokenFromRequest(Request);

                var userId = RequestProcessingHelper.GetIdFromToken(_jwtSettings.Value, token);

                if (userId == null)
                {
                    return BadRequest();
                }


                var quiz = await _quizService.SerachForNewQuizAsync(promptInput.Keyword.ToLower(), userId.Value, cancellation);

                if(quiz != null)
                {
                    return Ok(_mapper.Map<QuizDTO>(quiz));
                }
            }

            var newQuiz = await _quizService.GenerateQuizAsync(promptInput, cancellation);

            var result = await _quizService.CreateQuizAsync(newQuiz, cancellation);

            return Ok(_mapper.Map<QuizDTO>(result));
        }
    }
}
