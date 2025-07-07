using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViroCure.BLL.DTOs;
using ViroCure.BLL.IServices;
using ViroCure.DAL.Entities;
using ViroCure.DAL.IRepositories;

namespace ViroCure.BLL.Services
{
    public class LoginService : ILoginService
    {

        private readonly IUserRepo _repo;
        private readonly IConfiguration _configuration;


        public LoginService(IUserRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;

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


        public async Task<LoginResponseDto> LoginFunc(string email, string password)
        {
            var user = await _repo.LoginAsync(email, password);

            if (user == null) throw new Exception("Invalide email or password");

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Message = "Login successfull",
                Token = token,
                User = new UserDto
                {
                    Id = user.UserId,
                    Email = user.Email,
                    Role = user.Role switch { 1 => "Administrator", 2 => "Patients", 3  => "Doctor" }
                }
            };
            


        }
    }
}
