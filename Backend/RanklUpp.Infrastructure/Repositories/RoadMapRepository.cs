using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Models;
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

        public async Task<List<UserRoadMapItems>> AddedUserRoadMapItems(List<UserRoadMapItems> userRoadMapItems, CancellationToken cancellation = default)
        {
            await _context.UserRoadMapItems.AddRangeAsync(userRoadMapItems, cancellation);

            await _context.SaveChangesAsync(cancellation);

            return userRoadMapItems;
        }

        public async Task<RoadMap> CreateRoadMapAsync(RoadMap roadMap, CancellationToken cancellation = default)
        {
            var result =  _context.RoadMaps.Add(roadMap);

            await _context.SaveChangesAsync(cancellation);

            return result.Entity;
        }

        public async Task<RoadMap> GetRoadMapByIdAsync(int roadMapId, CancellationToken cancellation = default)
        {
            return await  _context.RoadMaps.Include(r => r.Items).FirstOrDefaultAsync(cancellation);
        }

        public async Task<List<UserRoadMapItems>> GetUserRoadMapItems(int userId, CancellationToken cancellation = default)
        {
            return await _context.UserRoadMapItems.Where(x => x.UserId == userId).ToListAsync(cancellation);
        }
    }
}
