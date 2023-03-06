 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Register
    {
        private Dictionary<string, Employee> register = new Dictionary<string, Employee>();

        public Dictionary<string, Employee> getAll()
        {
            return register;
        }

        public List<Employee> getAllList()
        {
            List<Employee> list = new List<Employee>();
            foreach (var kvp in register)
                list.Add(kvp.Value);
            return list;
        }

        public List<Employee> getOrder(Comparison<KeyValuePair<string, Employee>> comparator)
        {
            List<Employee> employees = new List<Employee>();
            List<KeyValuePair<string, Employee>> list = register.ToList();
            list.Sort(comparator);
            foreach (KeyValuePair<string, Employee> kvp in list)
                employees.Add(kvp.Value);
            return employees;
        }

        public void add(Employee employee)
        {
            register.Add(employee.id, employee);
        }

        public void remove(string id)
        {
            register.Remove(id);
        }

        public void addMany(List<Employee> employees)
        {
            foreach (Employee employee in employees)
                register.Add(employee.id, employee);
        }

        public List<Employee> filterByCity(string city)
        {
            List<Employee> employees = new List<Employee>();
            var toRet = register.Where(i => i.Value.adress.city.Equals(city));
            foreach (var employee in toRet)
                employees.Add(employee.Value);
            return employees; 
        }

        public Employee find(string id)
        {
            return register.FirstOrDefault(i => i.Key == id).Value;
        }

        public Dictionary<string, (Employee, double)> getValues()
        {
            Dictionary<string, (Employee, double)> employees = new Dictionary<string, (Employee, double)>();
            foreach (var kvp in register)
                employees.Add(kvp.Key, (kvp.Value, kvp.Value.getValue()));
            return employees;
        }

        public void clear()
        {
            register.Clear();
        }
    }
}
