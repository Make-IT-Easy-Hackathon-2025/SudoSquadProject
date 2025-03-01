using RankUpp.Core.Models;

namespace RankUpp.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);

        public Task<bool> IsEmailUsedAsync(string email, CancellationToken cancellationToken = default);

        public Task<bool> IsUserNameUsedAsync(string userName, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        public Task<int> UpdateUserScoreAsync(int userId,int newAddition, CancellationToken cancellationToken = default);

        public Task<int> GetUserActivityStreakAsync(int userId);
    }
}
