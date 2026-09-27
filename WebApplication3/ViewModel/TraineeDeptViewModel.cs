using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class TraineeDeptViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public double Grade { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}