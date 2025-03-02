using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class CreateRoadMapDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<CreateRoadMapItemDTO> Items { get; set; }
    }
}
