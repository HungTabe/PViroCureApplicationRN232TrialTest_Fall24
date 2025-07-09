using System.ComponentModel.DataAnnotations;

namespace ViroCureBLL.DTOs
{
    public class PersonResponseDto
    {
        public int PersonId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateOnly BirthDay { get; set; }
        public string Phone { get; set; } = string.Empty;
        public List<VirusResponseDto> Viruses { get; set; } = new List<VirusResponseDto>();
    }

    public class VirusResponseDto
    {
        public string VirusName { get; set; } = string.Empty;
        public double ResistanceRate { get; set; }
    }

    public class UpdatePersonRequestDto
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9@#\s]*[A-Z][a-zA-Z0-9@#\s]*$", 
            ErrorMessage = "Each word of the Fullname must begin with the capital letter")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        [RegularExpression(@"^\+84989\d{6}$", 
            ErrorMessage = "Phone number must be in the format +84989xxxxxx")]
        public string Phone { get; set; } = string.Empty;

        public List<VirusDto> Viruses { get; set; } = new List<VirusDto>();
    }

    public class ApiResponseDto
    {
        public string Message { get; set; } = string.Empty;
    }

} 