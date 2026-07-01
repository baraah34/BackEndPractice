using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{

    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int courseId { get; set; } // system generated

        [Required]
        [MaxLength(20)]
        public string courseCode { get; set; } = string.Empty; // user input, unique

        [Required]
        [MaxLength(150)]
        public string courseTitle { get; set; } = string.Empty; // user input

        [Required]
        [Range(1, 6)]
        public int creditHours { get; set; } // user input

        [Required]
        [ForeignKey("Department")]//class name 
        public int departmentId { get; set; } // foreign key


        [ForeignKey("Instructor")]//class name
        public int instructorId { get; set; } // foreign key

        [Required]
        [MaxLength(20)]
        public string semesterOffered { get; set; } = string.Empty; // user input, from list
    }
}