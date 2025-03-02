using RankUpp.Application.Models;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IRoadMapService
    {
        public Task<RoadMap> CreateRoadMapAsync(RoadMap roadMap, CancellationToken cancellation= default);

        public Task<RoadMap> GenerateRoadMapAsync(string keyword, CancellationToken cancellation = default);

        public Task<List<UserRoadMapItems>> ComplateRoadItmesAsync(List<int> ids, int userId,  CancellationToken cancellation = default);

        public Task<RoadMapRepayDTO> GetRoadMapReplayAsync(int userId, int roadMapId, CancellationToken cancellation = default);
    }
}
