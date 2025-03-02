using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    [Table("roadmap_items")]
    public class RoadMapItem
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }

        [ForeignKey(nameof(RoadMap))]
        public int RoadMapId { get; set; }

        public RoadMap RoadMap { get; set; }
    }
}
