using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class QuizDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("questions")]
        public List<QuizQuestionDTO> Questions { get; set; }

    }
}
