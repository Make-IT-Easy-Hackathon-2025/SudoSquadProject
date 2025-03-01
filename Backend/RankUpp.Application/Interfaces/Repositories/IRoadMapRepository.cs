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
    }
}
