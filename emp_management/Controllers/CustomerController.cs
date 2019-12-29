using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using emp_management.Models;
using emp_management.ViewModes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace emp_management.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHostingEnvironment _HostingEnvironment;
        private ICustomerRep _CustomerRep;

        public CustomerController(IHostingEnvironment _hostingEnvironment, ICustomerRep _customerRep)
        {
            _HostingEnvironment = _hostingEnvironment;
            _CustomerRep = _customerRep;
        }

        public ViewResult IndexCus()
        {
            IEnumerable<Customer> customers;
            customers = _CustomerRep.GetAllCustomer();
            return View(customers);
        }

        // [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            Customer cu = new Customer();

            cu = _CustomerRep.GetCustomer(id ?? 1);
            CustomerDetailsViewModel homeDetailsViewModelc = new CustomerDetailsViewModel()
            {

                Customer = cu,

                PageTitle = "Customer Details"
            };


            //return "Hello from MVC";
            //return Json(new { id = 1, name = "atip" });



            //ViewBag.Employee = em;
            //ViewData["PageTitle"] = "Emp Details";

            //return new ObjectResult(em);
            return View(homeDetailsViewModelc);

        }
        //public ViewResult Create()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel customer)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (customer.Photos != null && customer.Photos.Count > 0)
                {
                    foreach (IFormFile photo in customer.Photos)
                    {
                        string uploaderFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploaderFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                }

                Customer newCustomer = new Customer
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    PhotoPath = uniqueFileName
                };

                _CustomerRep.Add(newCustomer);
                return RedirectToAction("details", new { id = newCustomer.Id });
            }
            else
            {
                return View();
            }

        }
    }
}
