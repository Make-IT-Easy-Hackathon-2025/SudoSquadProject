using RankUpp.Core.DTOs.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class CreateQuizDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public List<CreateQuizQuestionDTO> Questions { get; set; }
    }
}
