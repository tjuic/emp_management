using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public class MockCustomerRep : ICustomerRep
    {
        private List<Customer> _customerList;

        public MockCustomerRep()
        {
            _customerList = new List<Customer>
            {
            new Customer() { Id = 1, Name = "FFF", Member = Mem.Silver, Email ="f@email.com"},
            new Customer() { Id = 2, Name = "SSS", Member = Mem.Bronze, Email = "s@email.com" },
            new Customer() { Id = 3, Name = "TTT", Member = Mem.Gold, Email = "t@email.com" }
            };
        }

        public Customer Add(Customer customer)
        {
            //throw new NotImplementedException();
            customer.Id = _customerList.Max(e => e.Id) + 1;
            _customerList.Add(customer);
            return customer;
        }

        public Customer Delete(int id)
        {
            Customer customer = _customerList.FirstOrDefault(e => e.Id == id);
            if (customer != null)
            {
                _customerList.Remove(customer);
            }
            return (customer);

        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            //throw new NotImplementedException();
            return (_customerList);
        }

        public Customer GetCustomer(int Id)
        {
            //throw new NotImplementedException();
            return _customerList.FirstOrDefault(a => a.Id == Id);
        }

        public Customer Update(Customer customerChanges)
        {
            Customer customer =  _customerList.FirstOrDefault(e => e.Id == customerChanges.Id);
            if (customer != null)
            {
                customer = customerChanges;
            }
            return (customer);

        }
    }
}
