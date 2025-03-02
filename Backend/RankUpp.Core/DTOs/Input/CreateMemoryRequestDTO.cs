using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace RankUpp.Core.DTOs.Input
{
    public class CreateMemoryRequestDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
