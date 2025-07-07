using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViroCureBLL.DTOs
{
    public class AddPersonRequestDto
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9@#\s]*$", 
            ErrorMessage = "Each word of the Fullname must begin with the capital letter")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        [RegularExpression(@"^\+84989\d{6}$", 
            ErrorMessage = "Phone number must be in the format +84989xxxxxx")]
        public string Phone { get; set; } = string.Empty;

        public List<VirusDto>? Viruses { get; set; } = new List<VirusDto>();
    }
}
