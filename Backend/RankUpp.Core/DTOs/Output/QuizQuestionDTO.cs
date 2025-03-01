using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class QuizQuestionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("options")]
        public List<QuizOptionDTO> Options { get; set; }
    }
}
