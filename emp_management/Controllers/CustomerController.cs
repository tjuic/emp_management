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

        public ViewResult IndexC()
        {
            IEnumerable<Customer> customers;
            customers = _CustomerRep.GetAllCustomer();
            return View(customers);
        }

        // [Route("Home/Details/{id?}")]
        public ViewResult DetailsC(int? id)
        {
            Customer cu = new Customer();

            cu = _CustomerRep.GetCustomer(id ?? 1);
            CustomerDetailsViewModel CustomerDetailsViewModel = new CustomerDetailsViewModel()
            {

                Customer = cu,

                PageTitle = "Customer Details"
            };


            //return "Hello from MVC";
            //return Json(new { id = 1, name = "atip" });



            //ViewBag.Employee = em;
            //ViewData["PageTitle"] = "Emp Details";

            //return new ObjectResult(em);
            return View(CustomerDetailsViewModel);

        }
        public ViewResult CreateC()
        {
            return View();
        }

        public ViewResult EditC(int id)
        {

            Customer customer = _CustomerRep.GetCustomer(id);
            CustomerEditViewModel customerEditViewModel = new CustomerEditViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Member = customer.Member,
                ExistingPhotoPath = customer.PhotoPath
            };
            return View(customerEditViewModel);
        }

        [HttpPost]
        public IActionResult EditC(CustomerEditViewModel customer)
        {
            if (ModelState.IsValid)
            {
                Customer cus = _CustomerRep.GetCustomer(customer.Id);
                cus.Name = customer.Name;
                cus.Email = customer.Email;
                cus.Member = customer.Member;
                if (customer.Photos != null)
                {
                    if (customer.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_HostingEnvironment.WebRootPath, "images",
                        customer.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    cus.PhotoPath = ProcessUploadFile(customer);
                }

                _CustomerRep.Update(cus);
                return RedirectToAction("indexc");
            }
            else
            {
                return View();
            }

        }

        private string ProcessUploadFile(CustomerCreateViewModel customer)
        {
            string uniqueFileName = null;
            if (customer.Photos != null && customer.Photos.Count > 0)

            {
                foreach (IFormFile photo in customer.Photos)
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
        public IActionResult CreateC(CustomerCreateViewModel customer)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(customer);


                Customer newCustomer = new Customer
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    PhotoPath = uniqueFileName,
                    Member = customer.Member
                };

                _CustomerRep.Add(newCustomer);
                return RedirectToAction("detailsc", new { id = newCustomer.Id });
            }
            else
            {
                return View();
            }

        }
    }
}
