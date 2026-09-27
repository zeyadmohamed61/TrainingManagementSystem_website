using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class ITIContext : DbContext
    {
        public ITIContext(DbContextOptions<ITIContext> options) : base(options){}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Instructor>()
            .HasOne(i => i.Course)
            .WithMany(c => c.Instructors)
            .HasForeignKey(i => i.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseResult>()
            .HasOne(cr => cr.Trainee)
            .WithMany(t => t.CourseResults)
            .HasForeignKey(cr => cr.TraineeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT", Manager = "Ahmed" },
                new Department { Id = 2, Name = "HR", Manager = "Sara" },
                new Department { Id = 3, Name = "Finance", Manager = "Omar" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "ASP.NET Core", Degree = "Intermediate", MinDegree = 50, CourseHours = 40, DepartmentId = 1 },
                new Course { Id = 2, Name = "HR Basics", Degree = "Beginner", MinDegree = 50, CourseHours = 20, DepartmentId = 2 },
                new Course { Id = 3, Name = "Accounting", Degree = "Intermediate", MinDegree = 50, CourseHours = 30, DepartmentId = 3 }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "Mona", Image = "inst1.jpg", Address = "Cairo", Email = "mona@gmail.com", PhoneNumber = "0100000000", Salary = 8000, DepartmentId = 1, CourseId = 1 },
                new Instructor { Id = 2, Name = "Youssef", Image = "inst2.jpg", Address = "Giza", Email = "youssef@gmail.com", PhoneNumber = "0100000001", Salary = 7000, DepartmentId = 2, CourseId = 2 },
                new Instructor { Id = 3, Name = "Laila", Image = "inst3.jpg", Address = "Alex", Email = "laila@gmail.com", PhoneNumber = "0110000002", Salary = 9000, DepartmentId = 3, CourseId = 3 }
            );

            modelBuilder.Entity<Trainee>().HasData(
                new Trainee { Id = 1, Name = "Karim", Image = "trainee1.jpg", PhoneNumber = "0111111111", Grade = 85, DepartmentId = 1 },
                new Trainee { Id = 2, Name = "Nour", Image = "trainee2.jpg", PhoneNumber = "0111111112", Grade = 90, DepartmentId = 2 },
                new Trainee { Id = 3, Name = "Hana", Image = "trainee3.jpg", PhoneNumber = "0111111113", Grade = 75, DepartmentId = 3 }
            );

            modelBuilder.Entity<CourseResult>().HasData(
                new CourseResult { Id = 1, Degree = 85, CourseId = 1, TraineeId = 1 },
                new CourseResult { Id = 2, Degree = 90, CourseId = 2, TraineeId = 2 },
                new CourseResult { Id = 3, Degree = 75, CourseId = 3, TraineeId = 3 }
            );
        }
    }
}
