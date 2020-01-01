using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public class SQLCustomerRep : ICustomerRep
    {
        private readonly AppDbContext context;

        public SQLCustomerRep(AppDbContext context)
        {
            this.context = context;
        }

        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return (customer);
        }

        public Customer Delete(int id)
        {
            //throw new NotImplementedException();
            Customer customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers;
        }

        public Customer GetCustomer(int Id)
        {
            //throw new NotImplementedException();
            Customer cust = context.Customers.FirstOrDefault(t => t.Id == Id);
            return (cust);
        }

        public Customer Update(Customer customerChanges)
        {
            //throw new NotImplementedException();
            Customer cust = context.Customers.FirstOrDefault(t => t.Id == customerChanges.Id);
            if (cust != null)
            {
                cust = customerChanges;
                context.SaveChanges();
                return cust;
            }
            else
            {
                return null;
            }
        }

        public Customer UpdateNew(Customer customerChanges)
        {
            var customer = context.Customers.Attach(customerChanges);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerChanges;
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return context.Customers;
        }
    }
}
