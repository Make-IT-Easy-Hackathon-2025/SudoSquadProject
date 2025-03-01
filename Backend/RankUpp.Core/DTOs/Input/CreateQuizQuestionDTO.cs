using RankUpp.Core.DTOs.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class CreateQuizQuestionDTO
    {
        public string Value { get; set; }

        public List<CreateQuizOptionDTO> Options { get; set; }
    }
}
