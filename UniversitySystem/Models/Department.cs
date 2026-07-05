using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int departmentId { get; set; } // system generated

        [Required]
        [MaxLength(100)]
        public string departmentName { get; set; } // user input, unique

        [MaxLength(50)]
        public string building { get; set; } // user input

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.00", "999999999.99")]
        public decimal budget { get; set; } // user input

        [ForeignKey("HeadInstructor")]
        public int headInstructorId { get; set; } // foreign key

        public Instructor HeadInstructor { get; set; } // relationship ==> one department head instructor

        public ICollection<Course> Courses { get; set; } = new List<Course>(); // relationship ==> one department has many courses
    }
}