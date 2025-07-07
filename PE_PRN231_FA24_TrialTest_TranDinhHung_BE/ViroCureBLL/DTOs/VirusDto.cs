using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViroCureBLL.DTOs
{
    public class VirusDto
    {
        [Required]
        public string VirusName { get; set; } = string.Empty;

        [Required]
        [Range(0, 1, ErrorMessage = "Resistance Rate: Must be between 0 and 1")]
        public double ResistanceRate { get; set; }
    }
}
