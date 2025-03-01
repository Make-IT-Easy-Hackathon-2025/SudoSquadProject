using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    [Table("roadmaps")]
    public class RoadMap
    {
        [Key]
        public int Id {  get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RoadMapItem> Items { get; set; }
    }
}
