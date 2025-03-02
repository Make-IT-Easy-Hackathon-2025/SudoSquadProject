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
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryRepository _memoryRepository;

        public MemoryService(IMemoryRepository memoryRepository)
        {
            _memoryRepository = memoryRepository;
        }

        public async Task<UserMemory> CreateMemoryAsync(UserMemory memory, CancellationToken cancellationToken)
        {
            return await _memoryRepository.CreateMemoryAsync(memory, cancellationToken);
        }

        public async Task<UserMemory> GenerateMemoryFromQuizAsync(int userId, int quizId, CancellationToken cancellationToken)
        {
            return await _memoryRepository.GenerateMemoryFromQuizAsync(userId, quizId, cancellationToken);
        }

        public async Task<UserMemory> GenerateMemoryFromRoadMapAsync(int userId, int roadMapId, CancellationToken cancellationToken)
        {
            return await _memoryRepository.GenerateMemoryFromRoadMapAsync(userId, roadMapId, cancellationToken);
        }

        public async Task<List<UserMemory>> GetUserMemoriesAsync(int userId, int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
        {
            return await _memoryRepository.GetUserMemoriesAsync(userId, pageNumber, pageSize, cancellationToken);
        }
    }
}
