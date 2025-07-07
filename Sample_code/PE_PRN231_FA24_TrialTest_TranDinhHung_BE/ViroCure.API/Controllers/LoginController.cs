using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViroCure.BLL.DTOs;
using ViroCure.BLL.IServices;

namespace ViroCure.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var response = await _service.LoginFunc(login.Email, login.Password);
                return Ok(response);

            } catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

    }
}
