using emp_management.Models;
using emp_management.ViewModes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _HostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment _hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _HostingEnvironment = _hostingEnvironment;
        }

        //public JsonResult Index()
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        public ViewResult Index()
        {
            IEnumerable<Employee> employees;
            employees = _employeeRepository.GetAllEmployee();
            return View(employees);
        }

        // [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            Employee em = new Employee();

            em = _employeeRepository.GetEmployee(id ?? 1);
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {

                Employee = em,

                PageTitle = "Employee Details"
            };


            //return "Hello from MVC";
            //return Json(new { id = 1, name = "atip" });



            //ViewBag.Employee = em;
            //ViewData["PageTitle"] = "Emp Details";

            //return new ObjectResult(em);
            return View(homeDetailsViewModel);

        }
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (employee.Photos != null && employee.Photos.Count > 0)
                {
                    foreach(IFormFile photo in employee.Photos)
                    {
                        string uploaderFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploaderFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    
                }

                Employee newEmployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }

        }
    }
}
