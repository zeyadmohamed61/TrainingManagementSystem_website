using WebApplication3.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class InstructorDeptViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Instructor Name")]
        public string Name { get; set; }

        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public double Salary { get; set; }

        public int DepartmentId { get; set; }
        public int CourseId { get; set; }

        public List<Department> DepartmentList { get; set; }
        public List<Course> CourseList { get; set; }
    }
}