using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class CourseResultViewModel
    {
        public int Id { get; set; }
        public double Degree { get; set; }
        public int CourseId { get; set; }
        public int TraineeId { get; set; }
        public List<Course> CourseList { get; set; }
        public List<Trainee> TraineeList { get; set; }
    }
}