using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class CourseDeptViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public double MinDegree { get; set; }
        public int CourseHours { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}