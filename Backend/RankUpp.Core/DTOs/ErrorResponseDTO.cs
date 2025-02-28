using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs
{
    public class ErrorResponseDTO
    {
        [JsonPropertyName("error")]
        public string Error {  get; set; }
        public ErrorResponseDTO(string error)
        {
            Error = error;
        }
    }
}
