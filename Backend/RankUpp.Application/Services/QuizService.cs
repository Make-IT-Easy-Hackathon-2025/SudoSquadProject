using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
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

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            return await _quizRepository.CreateQuizAsync(quiz, cancellationToken);
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
