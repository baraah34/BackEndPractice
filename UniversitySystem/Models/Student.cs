using System;


namespace UniversitySystem.Models
{
    public class Student
    {
   
        public int studentId { get; set; } 

     
        public string fullName { get; set; } 

    
        public string email { get; set; } 

      
        public string phoneNumber { get; set; } 

    
        public DateTime dateOfBirth { get; set; } 
        
        public int enrollmentYear { get; set; }

   
        public decimal gpa { get; set; }
    }
}