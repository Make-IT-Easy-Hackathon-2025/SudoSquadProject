using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> CreateUserAsync(RegistrationRequestDTO user, CancellationToken cancellationToken);

        public Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO, CancellationToken cancellationToken);
    }
}
