using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniversitySystem.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int enrollmentId { get; set; } // system generated

        [Required]
        [ForeignKey("Student")]//class name 
        public int studentId { get; set; } // foreign key

        [Required]
        [ForeignKey("Course")]//class name 
        public int courseId { get; set; } // foreign key

        [Required]
        public DateTime enrollmentDate { get; set; } // user input

        [MaxLength(3)]
        public string finalGrade { get; set; } // user input

        [Required]
        [MaxLength(20)]
        public string status { get; set; } = "In Progress"; // default value
                                                          
        public Student Student { get; set; }  // relationship ==> enrollment belongs to one student

       
        public Course Course { get; set; }  // relationship ==> enrollment belongs to one course


    }
}