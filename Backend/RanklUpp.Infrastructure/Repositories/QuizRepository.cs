using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RanklUpp.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly RankUppDbContext _context;

        public QuizRepository(RankUppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuizAttempt>> AddQuizAttemptsAsync(List<QuizAttempt> attempts, CancellationToken cancellationToken = default)
        {
            _context.QuizAttempts.AddRange(attempts);

            await _context.SaveChangesAsync(cancellationToken);

            return attempts;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            var quizEntity = _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync(cancellationToken);

            return quizEntity.Entity;
        }

        public Task<List<Quiz>> GetAllQuizsAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Quizzes.AsQueryable();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            query = query.Include(q => q.Questions)
                                    .ThenInclude(q => q.Options);

            return query.ToListAsync(cancellationToken);
        }

        public async Task<List<QuizAttempt>> GetQuizAttemptsAsync(int quizId, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.QuizAttempts.Where(qa => qa.UserId == userId && quizId == qa.QuizId).ToListAsync(cancellationToken);
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id, CancellationToken cancellation)
        {
            return await _context.Quizzes.Include(q => q.Questions)
                                    .ThenInclude(q => q.Options)
                                    .FirstOrDefaultAsync(q => q.Id == id, cancellationToken: cancellation);
        }

        public async Task<List<Quiz>> GetQuizesByIdsAsync(List<int> ids, CancellationToken cancellation = default)
        {
            return await _context.Quizzes.Where(q =>  ids.Contains(q.Id)).Include(q => q.Questions)
                                                                        .ThenInclude(q => q.Options)
                                                                        .ToListAsync(cancellation);
        }

        public async Task<List<Quiz>> SearchQuizByKeywordAsync(string keyword, CancellationToken cancellationToken = default)
        {
            return await _context.Quizzes.Where(q => q.Title.ToLower().Contains(keyword) || q.Description.ToLower().Contains(keyword))
                                                                        .Include(q => q.Questions)
                                                                        .ThenInclude(q => q.Options)
                                                                        .ToListAsync(cancellationToken);
        }

        public async Task<List<QuizAttempt>> GetAllQuizAttemptsAsync(int userId, List<int> QuizIds, CancellationToken cancellation = default)
        {
            return await _context.QuizAttempts.Where(qa => qa.UserId == userId && QuizIds.Contains(qa.QuizId)).ToListAsync(cancellation);
        }
    }
}
