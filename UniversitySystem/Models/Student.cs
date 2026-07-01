using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentId { get; set; } // system generated

        [Required]
        [MaxLength(100)]
        public string fullName { get; set; } = string.Empty; // user input

        [Required]
        [MaxLength(150)]
        public string email { get; set; } = string.Empty; // user input, unique

        [MaxLength(20)]
        public string phoneNumber { get; set; } // user input, optional

        [Required]
        public DateTime dateOfBirth { get; set; } // user input

        [Required]
        [Range(2000, 2030)]//years 
        public int enrollmentYear { get; set; } // user input

        [Column(TypeName = "decimal(3,2)")]//how will store gpa in database 
        [Range(typeof(decimal), "0.0", "4.0")]//gpa must be between 0.0 and 4.0
        public decimal gpa { get; set; } = 0.0m; // default value

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>(); // relationship ==> one student has many enrollments
    }
}