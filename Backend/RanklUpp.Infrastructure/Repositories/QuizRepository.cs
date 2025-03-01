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

        public async Task<Quiz?> GetQuizByIdAsync(int id, CancellationToken cancellation)
        {
            return await _context.Quizzes.Include(q => q.Questions)
                                    .ThenInclude(q => q.Options)
                                    .FirstOrDefaultAsync(q => q.Id == id, cancellationToken: cancellation);
        }
    }
}
