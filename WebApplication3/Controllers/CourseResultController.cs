using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class CourseResultController : Controller
    {
        private ITIContext context;
        public CourseResultController(ITIContext context)
        {
            this.context = context;
        }

        public IActionResult ShowAll()
        {
            return View(context.CourseResults.Include(cr => cr.Course).Include(cr => cr.Trainee).ToList());
        }

        public IActionResult Details(int id)
        {
            var courseResult = context.CourseResults.Include(cr => cr.Course).Include(cr => cr.Trainee).FirstOrDefault(cr => cr.Id == id);
            if (courseResult == null) return NotFound();
            return View(courseResult);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult SaveAdd(CourseResult courseResult)
        {
            if (courseResult.Degree > 0 && !string.IsNullOrEmpty(courseResult.CourseId.ToString()) && !string.IsNullOrEmpty(courseResult.TraineeId.ToString()))
            {
                context.CourseResults.Add(courseResult);
                context.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            return View("Add", courseResult);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var courseResult = context.CourseResults.FirstOrDefault(cr => cr.Id == id);
            if (courseResult == null) return NotFound();

            var vm = new CourseResultViewModel
            {
                Id = courseResult.Id,
                Degree = courseResult.Degree,
                CourseId = courseResult.CourseId,
                TraineeId = courseResult.TraineeId,
                CourseList = context.Courses.ToList(),
                TraineeList = context.Trainees.ToList()
            };

            return View("Edit", vm);
        }

        [HttpPost]
        public IActionResult SaveEdit(CourseResultViewModel courseResultVM)
        {
            if (courseResultVM.Degree <= 0 || courseResultVM.CourseId == 0 || courseResultVM.TraineeId == 0)
            {
                courseResultVM.CourseList = context.Courses.ToList();
                courseResultVM.TraineeList = context.Trainees.ToList();
                return View("Edit", courseResultVM);
            }

            var courseResult = context.CourseResults.FirstOrDefault(cr => cr.Id == courseResultVM.Id);
            courseResult.Degree = courseResultVM.Degree;
            courseResult.CourseId = courseResultVM.CourseId;
            courseResult.TraineeId = courseResultVM.TraineeId;

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var courseResult = context.CourseResults.Find(id);
            if (courseResult != null)
            {
                context.CourseResults.Remove(courseResult);
                context.SaveChanges();
            }
            return RedirectToAction("ShowAll");
        }
    }
}