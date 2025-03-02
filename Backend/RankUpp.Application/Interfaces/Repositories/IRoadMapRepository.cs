using RankUpp.Application.Models;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Repositories
{
    public interface IRoadMapRepository
    {
        public Task<RoadMap> CreateRoadMapAsync(RoadMap roadMap, CancellationToken cancellation=default);

        public Task<List<UserRoadMapItems>> AddedUserRoadMapItems(List<UserRoadMapItems> userRoadMapItems, CancellationToken cancellation=default);

        public Task<RoadMap> GetRoadMapByIdAsync(int roadMapId, CancellationToken cancellation = default);

        public Task<List<UserRoadMapItems>> GetUserRoadMapItems(int userId, CancellationToken cancellation = default); 
    }
}
