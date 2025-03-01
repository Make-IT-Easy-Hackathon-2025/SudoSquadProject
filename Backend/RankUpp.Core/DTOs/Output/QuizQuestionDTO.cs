using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class QuizQuestionDTO
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public List<QuizOptionDTO> Options { get; set; }
    }
}
