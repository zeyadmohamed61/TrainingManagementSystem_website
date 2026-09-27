using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DepartmentController : Controller
    {
        private ITIContext context;
        public DepartmentController(ITIContext context)
        {
            this.context = context;
        }

        public IActionResult ShowAll()
        {
            return View(context.Departments.ToList());
        }

        public IActionResult Details(int id)
        {
            var department = context.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null) return NotFound();

            ViewData["Id"] = department.Id;
            ViewData["Name"] = department.Name;
            ViewData["Manager"] = department.Manager;
            ViewData["Message"] = "Hello From Department Controller";
            ViewBag.Temperature = 35;
            ViewBag.Branches = new List<string> { "ITI New Capital", "ITI Smart Village", "ITI Alex" };

            return View("Details");
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult SaveAdd(Department department)
        {
            if (!string.IsNullOrEmpty(department.Name) && !string.IsNullOrEmpty(department.Manager))
            {
                context.Departments.Add(department);
                context.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            return View("Add", department);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = context.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null) return NotFound();
            return View("Edit", department);
        }

        [HttpPost]
        public IActionResult SaveEdit(Department department)
        {
            if (string.IsNullOrEmpty(department.Name) || string.IsNullOrEmpty(department.Manager))
                return View("Edit", department);

            var dept = context.Departments.FirstOrDefault(d => d.Id == department.Id);
            if (dept == null) return NotFound();

            dept.Name = department.Name;
            dept.Manager = department.Manager;

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var department = context.Departments.Find(id);
            if (department != null)
            {
                context.Departments.Remove(department);
                context.SaveChanges();
            }
            return RedirectToAction("ShowAll");
        }
    }
}