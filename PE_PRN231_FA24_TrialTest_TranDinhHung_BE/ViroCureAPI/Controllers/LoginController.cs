using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViroCureBLL.DTOs;
using ViroCureBLL.IServices;

namespace ViroCureAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService personService)
        {
            _loginService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _loginService.LoginFunction(loginDto.Email, loginDto.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
