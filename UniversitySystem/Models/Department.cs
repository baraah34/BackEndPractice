

namespace UniversitySystem.Models
{
    public class Department
    {
        public int departmentId { get; set; } 

      
        public string departmentName { get; set; } 

        public string building { get; set; } 

        
        public decimal budget { get; set; } 

        
        public int? headInstructorId { get; set; } 
    }
}