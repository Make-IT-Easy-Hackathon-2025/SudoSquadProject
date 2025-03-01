using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserRepository _userRepository;
        public StatisticsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserStatistics> CalculateUserStatistics(int userId)
        {
            var userStatistics = new UserStatistics();

            userStatistics.StreakNumber = await _userRepository.GetUserActivityStreakAsync(userId);

            userStatistics.LastWewkActivityCount = await _userRepository.GetPointsChangeByTimeAsync(userId, DateTime.UtcNow.AddDays(-7));

            userStatistics.Score = await _userRepository.GetUserScoreByIdAsync(userId);

            return userStatistics;
        }
    }
}
