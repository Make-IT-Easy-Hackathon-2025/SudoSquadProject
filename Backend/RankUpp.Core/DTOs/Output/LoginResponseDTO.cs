using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Output
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
