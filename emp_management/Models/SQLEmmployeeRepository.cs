using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public class SQLEmmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLEmmployeeRepository> logger;

        public SQLEmmployeeRepository(AppDbContext context, 
                                      ILogger<SQLEmmployeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
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
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            //throw new NotImplementedException();
            Employee empl = context.Employees.FirstOrDefault(t => t.Id == Id);
            return (empl);
        }

        public Employee Update(Employee employeeChanges)
        {
            //throw new NotImplementedException();
            Employee empl = context.Employees.FirstOrDefault(t => t.Id == employeeChanges.Id);
            if (empl != null)
            {
                empl = employeeChanges;
                context.SaveChanges();
                return empl;
            }
            else
            {
                return null;
            }
        }

        public Employee UpdateNew(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
