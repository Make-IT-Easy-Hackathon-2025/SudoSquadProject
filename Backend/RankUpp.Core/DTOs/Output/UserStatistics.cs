﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class UserStatistics
    {
        public int StreakNumber { get; set; }

        public int LastWeekPoints { get; set; }

        public int LastWewkActivityCount { get; set; }
    }
}
