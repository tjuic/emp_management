using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public class SQLEmmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return (employee);
        }

        public Employee Delete(int id)
        {
            //throw new NotImplementedException();
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int Id)
        {
            //throw new NotImplementedException();
            return (context.Employees);
        }

        public Employee Update(Employee employeeChanges)
        {
            throw new NotImplementedException();
        }
    }
}
