using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    public class RoadMapDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<RoadMapItemDTO> Items  { get; set; }
    }
}
