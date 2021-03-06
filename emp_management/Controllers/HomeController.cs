﻿using emp_management.Models;
using emp_management.ViewModes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository, 
                              IHostingEnvironment _hostingEnvironment,
                              ILogger<HomeController> logger)
        {           
            _employeeRepository = employeeRepository;
            _HostingEnvironment = _hostingEnvironment;
            this.logger = logger;
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
            // throw new Exception("Error in Details View");

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }


            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {

                Employee = employee,

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

        public ViewResult Edit(int id)
        {

            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

       
             [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employee)
        {
            if (ModelState.IsValid)
            { 
            Employee emp = _employeeRepository.GetEmployee(employee.Id);
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Department = employee.Department;
                if (employee.Photos != null)
                {
                    if (employee.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_HostingEnvironment.WebRootPath, "images",
                        employee.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    emp.PhotoPath = ProcessUploadFile(employee);
                }

                _employeeRepository.Update(emp);
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }

        private string ProcessUploadFile(EmployeeCreateViewModel employee)
        {
            string uniqueFileName = null;
            if (employee.Photos != null && employee.Photos.Count > 0)
            {
                foreach (IFormFile photo in employee.Photos)
                {
                    string uploaderFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploaderFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);

                    }
                }

            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(employee);

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
