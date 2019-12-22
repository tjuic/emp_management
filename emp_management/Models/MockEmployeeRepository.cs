using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
            new Employee() {Id =1, Name="OneFName", Department= Dept.HR, Email="Emp1@email.com"},
            new Employee() { Id = 2, Name = "TwoFName", Department = Dept.IT, Email = "Emp2@email.com" },
            new Employee() { Id = 3, Name = "ThreeFName", Department = Dept.IT, Email = "Emp3@email.com" }
            };
        }

        public Employee Add(Employee employee)
        {
            //throw new NotImplementedException();
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return (employee);

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            //throw new NotImplementedException();
            return (_employeeList);
        }

        public Employee GetEmployee(int Id)
        {
            //throw new NotImplementedException();
            return _employeeList.FirstOrDefault(a => a.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee = employeeChanges;
            }
            return (employee);

        }
    }
}
