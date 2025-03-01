using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input.Gemini
{
    public class ContentRequest
    {
        [JsonPropertyName("contents")]
        public Content[] Contents { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("parts")]
        public Part[] Parts { get; set; }
    }
    public class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
