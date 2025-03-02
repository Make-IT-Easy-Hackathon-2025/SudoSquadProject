using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IMemoryService
    {
        public Task<UserMemory> CreateMemoryAsync(UserMemory memory, CancellationToken cancellationToken);

        public Task<UserMemory> GenerateMemoryFromQuizAsync(int userId, int quizId, CancellationToken cancellationToken);

        public Task<List<UserMemory>> GetUserMemoriesAsync(int userId, int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default);

        public Task<UserMemory> GenerateMemoryFromRoadMapAsync(int userId, int roadMapId, CancellationToken cancellationToken);
    }
}
