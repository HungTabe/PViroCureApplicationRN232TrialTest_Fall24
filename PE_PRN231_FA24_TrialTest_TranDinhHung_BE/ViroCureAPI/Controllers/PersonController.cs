using Microsoft.AspNetCore.Mvc;
using ViroCureBLL.DTOs;
using ViroCureBLL.IServices;

namespace ViroCureAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("person")]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { error = string.Join(", ", errors) });
                }

                var response = await _personService.AddPersonAsync(request);
                response.PersonId = request.PersonId;
                return StatusCode(201, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred" });
            }
        }

        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            try
            {
                var person = await _personService.GetPersonAsync(id);
                if (person == null)
                {
                    return NotFound(new { error = "Person not found" });
                }
                return Ok(person);
            }
            catch
            {
                return StatusCode(500, new { error = "An unexpected error occurred" });
            }
        }

        [HttpGet("persons")]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                var persons = await _personService.GetAllPersonsAsync();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred" });
            }
        }

    }
} 