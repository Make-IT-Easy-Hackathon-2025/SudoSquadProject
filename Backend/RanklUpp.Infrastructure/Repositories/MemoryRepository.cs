using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Core.Exceptions;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RanklUpp.Infrastructure.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly RankUppDbContext _context;

        public MemoryRepository(RankUppDbContext context)
        {
            _context = context;
        }

        public async Task<UserMemory> CreateMemoryAsync(UserMemory memory, CancellationToken cancellationToken)
        {
            memory.Date = DateTime.UtcNow;

            var result = await _context.Memories.AddAsync(memory, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<UserMemory> GenerateMemoryFromQuizAsync(int userId, int quizId, CancellationToken cancellationToken)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);

            if (quiz == null)
            {
                throw new InvalidIdException();
            }

            var memory = new UserMemory
            {
                UserId = userId,
                QuizId = quizId,
                Date = DateTime.UtcNow,
                Title = "Quiz: " + quiz.Title,
                Description = quiz.Description,
            };

            var result = await _context.Memories.AddAsync(memory);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<UserMemory> GenerateMemoryFromRoadMapAsync(int userId, int roadMapId, CancellationToken cancellationToken)
        {
            var roadMap = await _context.RoadMaps.FindAsync(roadMapId);

            if (roadMap == null)
            {
                throw new InvalidIdException();
            }

            var memory = new UserMemory
            {
                UserId = userId,
                RoadMapId = roadMapId,
                Date = DateTime.UtcNow,
                Title = "Roadmap: " + roadMap.Title,
                Description = roadMap.Description,
            };

            var result = await _context.Memories.AddAsync(memory);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public Task<List<UserMemory>> GetUserMemoriesAsync(int userId,int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Memories.Where(m => m.UserId == userId);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToListAsync(cancellationToken);
        }
    }
}
