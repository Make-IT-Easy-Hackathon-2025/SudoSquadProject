using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.Exceptions;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        private readonly IUserRepository _userRepository;

        public QuizService(IQuizRepository quizRepository, IUserRepository userRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<List<QuizAttempt>> AddQuizAttemptsAsync(List<QuizAttempt> attempts, CancellationToken cancellationToken = default)
        {
            return await _quizRepository.AddQuizAttemptsAsync(attempts, cancellationToken);
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            return await _quizRepository.CreateQuizAsync(quiz, cancellationToken);
        }

        public async Task<int> EvaluateQuizAsync(int quizId, int userId, CancellationToken cancellationToken = default)
        {
            var quizes = await _quizRepository.GetQuizByIdAsync(quizId, cancellationToken);

            if(quizes == null)
            {
                throw new InvalidIdException();
            }

            var answers = await _quizRepository.GetQuizAttemptsAsync(quizId, userId, cancellationToken);

            int rightAnswers = 0;

            foreach (var item in quizes.Questions)
            {
                foreach (var option in item.Options)
                {
                    if(option.IsCorrect && answers.Any(x => x.QuizOptionId == option.Id))
                    {
                        rightAnswers++;
                    }
                }
            }

            await _userRepository.UpdateUserScoreAsync(userId, rightAnswers - answers.Count);

            return rightAnswers - answers.Count;
        }

        public async Task<List<Quiz>> GetAllQuizsAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            return await _quizRepository.GetAllQuizsAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id, CancellationToken cancellation)
        {
            return await _quizRepository.GetQuizByIdAsync(id, cancellation);
        }
    }
}
