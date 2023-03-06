using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ConcreteEmployees
{
    public class OfficeEmployee : Employee
    {
        private static int idCount { get; set; }
        public string officeId { get; }
        private int _iq;
        public int iq
        {
            get => _iq;
            set
            {
                if (value < 70) _iq = 70;
                else if (value > 150) _iq = 150;
                else _iq = value;
            }
        }
        public OfficeEmployee(string firstName, string lastName, int age, double experience, Adress adress, int iq) : base(firstName, lastName, age, experience, adress)
        {
            this.iq = iq;
            this.officeId = 'O' + (++idCount).ToString();
        }

        public override double getValue() => iq * experience;
    }
}
