using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.DTOs.Input
{
    public class RegistrationRequestDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
