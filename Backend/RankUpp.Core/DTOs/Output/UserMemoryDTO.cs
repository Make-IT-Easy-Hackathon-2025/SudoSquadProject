﻿using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class UserMemoryDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime Date { get; set; }

        public int? QuizId { get; set; }

        public int? RoadMapId { get; set; }
    }
}
