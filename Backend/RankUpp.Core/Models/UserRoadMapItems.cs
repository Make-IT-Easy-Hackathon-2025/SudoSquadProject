using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Models
{
    [Table("user_roadmap_items")]
    public class UserRoadMapItems
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int RoadMapItemId { get; set; }

        public virtual RoadMapItem RoadMapItem { get; set; }
    }
}
