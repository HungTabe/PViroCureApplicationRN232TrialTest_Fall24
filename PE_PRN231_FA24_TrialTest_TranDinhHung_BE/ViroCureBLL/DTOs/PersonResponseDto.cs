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

} 