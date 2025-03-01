using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Core.Models;

namespace RanklUpp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly RankUppDbContext _context;

        public UserRepository(RankUppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var result = await _context.AddAsync(user);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<int> GetUserActivityStreakAsync(int userId)
        {
            var quizAttempts = await _context.QuizAttempts
                                            .Where(q => q.UserId == userId)
                                            .OrderBy(q => q.Date)
                                            .Select(q => q.Date)
                                            .ToListAsync();

            var userMemories = await _context.Memories
                                            .Where(m => m.UserId == userId)
                                            .OrderBy(m => m.Date)
                                            .Select(m => m.Date)
                                            .ToListAsync();

  
            var activityDates = quizAttempts.Concat(userMemories)
                                            .Distinct() 
                                            .OrderBy(d => d) 
                                            .ToList();

            int streakCount = 0;
            int maxStreak = 0;
            DateTime? previousDate = null;

            foreach (var activityDate in activityDates)
            {
                if (previousDate.HasValue && (activityDate - previousDate.Value).Days == 1)
                {
                    streakCount++;
                }
                else
                {
                    streakCount = 1;
                }

                maxStreak = Math.Max(maxStreak, streakCount);
                previousDate = activityDate;
            }

            return maxStreak;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<QuizAttempt>> GetUserActivitiesForLastWeekAsync(int userId)
        {
            DateTime oneWeekAgo = DateTime.UtcNow.Date.AddDays(-7);

           return await _context.QuizAttempts.Where(q => q.UserId == userId && q.Date >= oneWeekAgo).ToListAsync();
        }

        public async Task<bool> IsEmailUsedAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsUserNameUsedAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> UpdateUserScoreAsync(int userId, int newAddition, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new InvalidDataException();
            }

            user.Score += newAddition;

            _context.SaveChanges();

            return user.Score;
        }

        public async Task<int> GetPointsChangeByTimeAsync(int userId, DateTime dateTime)
        {
            var attempts = await _context.QuizAttempts.Where(qa => qa.UserId == userId && qa.Date > dateTime).Include(att => att.QuizOption).ToListAsync();

            int score = 0;

            foreach (var item in attempts)
            {
                if (item.QuizOption.IsCorrect)
                {
                    score++;
                }
                else
                {
                    score--;
                }
            }
            return score;
        }

        public async Task<int> GetUserScoreByIdAsync(int userId)
        {
            return (await _context.Users.FindAsync(userId)).Score;
        }
    }
}
