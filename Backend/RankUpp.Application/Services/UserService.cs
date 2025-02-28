using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RankUpp.Api.Configurations;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Application.Interfaces.Services;
using RankUpp.Core.DTOs.Input;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Exceptions;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IOptions<JwtSettings> _jwtSettings;

        public UserService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;

            _jwtSettings = jwtSettings;
        }

        public async Task<LoginResponseDTO> CreateUserAsync(RegistrationRequestDTO userDTO, CancellationToken cancellationToken)
        {
            if(await _userRepository.IsUserNameUsedAsync(userDTO.UserName, cancellationToken))
            {
                throw new UserNameAlreadyUsedException();
            }

            if(await _userRepository.IsEmailUsedAsync(userDTO.Email, cancellationToken))
            {
                throw new EmailAlreadyInUseException();
            }

            var user = new User { Email = userDTO.Email, UserName = userDTO.UserName, PasswordHash = HashPassword(userDTO.Password) };
            
            var result = await _userRepository.CreateUserAsync(user, cancellationToken);

            return new LoginResponseDTO
            {
                Id = result.Id,
                Email = result.Email,
                Username = result.UserName,
                Token = CreateToken(result),
            };
        }


        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            string? secretKey = _jwtSettings.Value.TokenKey;


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequestDTO.Email, cancellationToken);

            if (user == null)
            {
                throw new WrongEmailOrPasswordException();
            }

            if (VerifyPassword(loginRequestDTO.Password, user.PasswordHash) == false)
            {
                throw new WrongEmailOrPasswordException();
            }

            return new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = CreateToken(user),
            };
        }
    }
}
