using System;

namespace UniversitySystem.Models
{
    public class Instructor
    {
        
        public int instructorId { get; set; } 

      
        public string fullName { get; set; } 

      
        public string email { get; set; } 

        public string officeNumber { get; set; } 

        public DateTime hireDate { get; set; } 

        public decimal salary { get; set; } 

        public string academicTitle { get; set; } 
    }
}