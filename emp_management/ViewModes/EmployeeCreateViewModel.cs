﻿using emp_management.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.ViewModes
{
    public class EmployeeCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
