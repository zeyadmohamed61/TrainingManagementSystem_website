using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class TraineeController : Controller
    {
        private ITIContext context;
        public TraineeController(ITIContext context)
        {
            this.context = context;
        }

        public IActionResult ShowAll()
        {
            return View(context.Trainees.ToList());
        }

        public IActionResult Details(int id)
        {
            var trainee = context.Trainees.Include(t => t.Department).FirstOrDefault(t => t.Id == id);
            if (trainee == null) return NotFound();

            var vm = new TraineeViewModel
            {
                TraineeName = trainee.Name,
                TraineeImage = trainee.Image,
                DepartmentName = trainee.Department.Name,
                DepartmentId = trainee.DepartmentId,
                Advertisement = "Register for our next training batch, free of charge",
                Color = "Red",
                Branches = new List<string> { "ITI New Capital", "ITI Smart Village", "ITI Alex" },
                Temp = 25
            };

            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult SaveAdd(Trainee trainee)
        {
            if (!string.IsNullOrEmpty(trainee.Name) && !string.IsNullOrEmpty(trainee.Image)
                && trainee.Grade > 0 && trainee.DepartmentId > 0)
            {
                context.Trainees.Add(trainee);
                context.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            return View("Add", trainee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trainee = context.Trainees.FirstOrDefault(t => t.Id == id);
            if (trainee == null) return NotFound();

            var traineeVM = new TraineeDeptViewModel
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Image = trainee.Image,
                PhoneNumber = trainee.PhoneNumber,
                Grade = trainee.Grade,
                DepartmentId = trainee.DepartmentId,
                DepartmentList = context.Departments.ToList()
            };

            return View("Edit", traineeVM);
        }

        [HttpPost]
        public IActionResult SaveEdit(TraineeDeptViewModel traineeVM)
        {
            if (string.IsNullOrEmpty(traineeVM.Name) || traineeVM.Grade <= 0 || traineeVM.DepartmentId == 0)
            {
                traineeVM.DepartmentList = context.Departments.ToList();
                return View("Edit", traineeVM);
            }

            var trainee = context.Trainees.FirstOrDefault(t => t.Id == traineeVM.Id);
            trainee.Name = traineeVM.Name;
            trainee.Image = traineeVM.Image;
            trainee.PhoneNumber = traineeVM.PhoneNumber;
            trainee.Grade = traineeVM.Grade;
            trainee.DepartmentId = traineeVM.DepartmentId;

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trainee = context.Trainees.Find(id);
            if (trainee != null)
            {
                context.Trainees.Remove(trainee);
                context.SaveChanges();
            }
            return RedirectToAction("ShowAll");
        }
    }
}