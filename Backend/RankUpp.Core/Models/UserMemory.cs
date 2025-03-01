﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    [Table("memories")]
    public class UserMemory
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int? QuizId { get; set; }

        [ForeignKey(nameof(QuizId))]
        public Quiz? Quiz { get; set; }
    }
}
