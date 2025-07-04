using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViroCureBLL.DTOs;
using ViroCureBLL.IServices;
using ViroCureDAL.Entities;
using ViroCureDAL.IRepositories;

namespace ViroCureBLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IPersonRepository _repo;
        private readonly IConfiguration _configuration;


        public LoginService(IPersonRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginFunction(string email, string password)
        {
            var user = await _repo.LoginAsync(email, password);

            if (user == null) throw new Exception("Invalid email or password");

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Message = "Login successful",
                Token = token,
                User = new UserDto
                {
                    Id = user.UserId,
                    Email = user.Email,
                    Role = user.Role switch { 1 => "admin", 2 => "patient", 3 => "doctor", _ => "unknown" }
                }
            };
        }

        private string GenerateJwtToken(ViroCureUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpiryInDays"])),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
