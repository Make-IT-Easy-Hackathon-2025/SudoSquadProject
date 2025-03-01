using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class QuizOptionReplayDTO
    {
        public int Id { get; set; }

        public string OptionValue { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsSelected { get; set; }
    }
}
