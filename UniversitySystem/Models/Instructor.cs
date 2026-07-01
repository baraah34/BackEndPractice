using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int instructorId { get; set; } // system generated

        [Required]
        [MaxLength(100)]
        public string fullName { get; set; } = string.Empty; // user input

        [Required]
        [MaxLength(150)]
        public string email { get; set; } = string.Empty; // user input, unique

        [MaxLength(20)]
        public string officeNumber { get; set; } // user input, optional

        [Required]
        public DateTime hireDate { get; set; } // user input

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.01", "999999999.99")]
        public decimal salary { get; set; } // user input

        [Required]
        [MaxLength(50)]
        public string academicTitle { get; set; } = string.Empty; // user input, from list
    }
}