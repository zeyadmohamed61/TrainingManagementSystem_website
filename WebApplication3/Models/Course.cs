using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public double MinDegree { get; set; }
        public int CourseHours { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Instructor> Instructors { get; set; }
        public List<CourseResult> CourseResults { get; set; }
    }
}
