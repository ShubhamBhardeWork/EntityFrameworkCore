
namespace MyProject.Models.Domain
{
    public class Employee
    {
        // public int Id {get; set;}
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Position {get; set;}
        public decimal Salary {get; set;}
    }
}