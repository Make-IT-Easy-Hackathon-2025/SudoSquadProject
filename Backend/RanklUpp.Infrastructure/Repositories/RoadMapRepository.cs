using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RanklUpp.Infrastructure.Repositories
{
    public class RoadMapRepository : IRoadMapRepository
    {
        private readonly RankUppDbContext _context;

        public RoadMapRepository(RankUppDbContext context)
        {
            _context = context;
        }

        public async Task<RoadMap> CreateRoadMapAsync(RoadMap roadMap, CancellationToken cancellation = default)
        {
            var result =  _context.RoadMaps.Add(roadMap);

            await _context.SaveChangesAsync(cancellation);

            return result.Entity;
        }
    }
}
