using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public Candidate[] Candidates { get; set; }
    }

    public sealed class Candidate
    {
        [JsonPropertyName("content")]
        public Content Content { get; set; }
    }

    public sealed class Content
    {
        [JsonPropertyName("parts")]
        public Part[] Parts { get; set; }
        public string Role {get; set;}
    }

    public sealed class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
