using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IQuizService
    {
        public Task<Quiz> CreateQuizAsync(Quiz quiz, CancellationToken cancellationToken);

        public Task<Quiz?> GetQuizByIdAsync(int id, CancellationToken cancellation);

        public Task<List<Quiz>> GetAllQuizsAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default);

        public Task<List<QuizAttempt>> AddQuizAttemptsAsync(List<QuizAttempt> attempts, CancellationToken cancellationToken = default);

        public Task<int> EvaluateQuizAsync(int quizId, int userId, CancellationToken cancellationToken = default);

        public Task<QuizReplayDTO> GetQuizReplayByIdAsync(int quizId, int userId, CancellationToken cancellationToken = default);
    }
}
