using RankUpp.Core.DTOs.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IStatisticsService
    {
        public Task<UserStatistics> CalculateUserStatistics(int userId);
    }
}
