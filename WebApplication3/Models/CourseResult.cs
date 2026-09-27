using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class CourseResult
    {
        public int Id { get; set; }
        public double Degree { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey(nameof(Trainee))]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }
    }
}
