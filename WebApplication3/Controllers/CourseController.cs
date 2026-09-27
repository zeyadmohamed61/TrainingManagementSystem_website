using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class CourseController : Controller
    {
        private ITIContext context;
        public CourseController(ITIContext context)
        {
            this.context = context;
        }

        public IActionResult ShowAll()
        {
            return View(context.Courses.ToList());
        }

        public IActionResult Details(int id)
        {
            var course = context.Courses.Include(c => c.Department).Include(c => c.Instructors).FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult SaveAdd(Course course)
        {
            if (!string.IsNullOrEmpty(course.Name) && !string.IsNullOrEmpty(course.Degree)
                && course.MinDegree > 0 && course.CourseHours > 0 && course.DepartmentId > 0)
            {
                context.Courses.Add(course);
                context.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            return View("Add", course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();

            var courseVM = new CourseDeptViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                CourseHours = course.CourseHours,
                DepartmentId = course.DepartmentId,
                DepartmentList = context.Departments.ToList()
            };

            return View("Edit", courseVM);
        }

        [HttpPost]
        public IActionResult SaveEdit(CourseDeptViewModel courseVM)
        {
            if (string.IsNullOrEmpty(courseVM.Name) || string.IsNullOrEmpty(courseVM.Degree)
                || courseVM.MinDegree <= 0 || courseVM.CourseHours <= 0 || courseVM.DepartmentId == 0)
            {
                courseVM.DepartmentList = context.Departments.ToList();
                return View("Edit", courseVM);
            }

            var course = context.Courses.FirstOrDefault(c => c.Id == courseVM.Id);
            course.Name = courseVM.Name;
            course.Degree = courseVM.Degree;
            course.MinDegree = courseVM.MinDegree;
            course.CourseHours = courseVM.CourseHours;
            course.DepartmentId = courseVM.DepartmentId;

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = context.Courses.Find(id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            return RedirectToAction("ShowAll");
        }
    }
}