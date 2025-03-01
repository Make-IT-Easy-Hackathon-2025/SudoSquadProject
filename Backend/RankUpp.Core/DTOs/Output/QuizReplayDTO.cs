using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class QuizReplayDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionReplayDTO> Questions { get; set; } 
    }
}
