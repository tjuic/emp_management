using emp_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.ViewModes
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public string PageTitle { get; set; }
        //public CurrentProject currentProject { get; set; }
    }
}
