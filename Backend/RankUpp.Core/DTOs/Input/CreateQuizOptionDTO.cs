using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class CreateQuizOptionDTO
    {
        public string OptionValue { get; set; }

        public bool IsCorrect { get; set; }
    }
}
