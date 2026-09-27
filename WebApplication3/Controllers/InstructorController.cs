using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class InstructorController : Controller
    {
        private ITIContext context;
        public InstructorController(ITIContext context)
        {
            this.context = context;
        }

        public IActionResult ShowAll()
        {
            return View(context.Instructors.ToList());
        }

        public IActionResult Details(int id)
        {
            var instructor = context.Instructors.Include(i => i.Department).Include(i => i.Course).FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult SaveAdd(Instructor instructor)
        {
            if (!string.IsNullOrEmpty(instructor.Name) && !string.IsNullOrEmpty(instructor.Image)
                && !string.IsNullOrEmpty(instructor.Address) && !string.IsNullOrEmpty(instructor.Email)
                && !string.IsNullOrEmpty(instructor.PhoneNumber) && instructor.Salary > 0
                && instructor.CourseId > 0 && instructor.DepartmentId > 0)
            {
                context.Instructors.Add(instructor);
                context.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            return View("Add", instructor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();

            var instructorVM = new InstructorDeptViewModel
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Image = instructor.Image,
                Address = instructor.Address,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
                Salary = instructor.Salary,
                DepartmentId = instructor.DepartmentId,
                CourseId = instructor.CourseId,
                DepartmentList = context.Departments.ToList(),
                CourseList = context.Courses.ToList()
            };

            return View("Edit", instructorVM);
        }

        [HttpPost]
        public IActionResult SaveEdit(InstructorDeptViewModel instructorVM)
        {
            if (instructorVM == null)
                return RedirectToAction("ShowAll");

            if (string.IsNullOrEmpty(instructorVM.Name) || string.IsNullOrEmpty(instructorVM.Email) || instructorVM.DepartmentId == 0 || instructorVM.CourseId == 0)
            {
                instructorVM.DepartmentList = context.Departments.ToList();
                instructorVM.CourseList = context.Courses.ToList();
                return View("Edit", instructorVM);
            }

            var instructorFromDb = context.Instructors.FirstOrDefault(i => i.Id == instructorVM.Id);
            instructorFromDb.Name = instructorVM.Name;
            instructorFromDb.Image = instructorVM.Image;
            instructorFromDb.Address = instructorVM.Address;
            instructorFromDb.Email = instructorVM.Email;
            instructorFromDb.PhoneNumber = instructorVM.PhoneNumber;
            instructorFromDb.Salary = instructorVM.Salary;
            instructorFromDb.DepartmentId = instructorVM.DepartmentId;
            instructorFromDb.CourseId = instructorVM.CourseId;

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var instructor = context.Instructors.Find(id);
            if (instructor != null)
            {
                context.Instructors.Remove(instructor);
                context.SaveChanges();
            }
            return RedirectToAction("ShowAll");
        }
    }
}