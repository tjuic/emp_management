using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public interface ICustomerRep
    {
        Customer GetCustomer(int Id);
        IEnumerable<Customer> GetAllCustomer();
        Customer Add(Customer customer);
        Customer Update(Customer customerChanges);
        Customer Delete(int id);
    }
}
