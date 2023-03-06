using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ConcreteEmployees
{
    public class Trader : Employee
    {
        private int _commision;
        public int commision
        {
            get => _commision;
            set
            {
                if (value < 1) _commision = 1;
                else if (value > 100) _commision = 100;
                else _commision = value;
            }
        }

        public Efficiency efficiency { get; set; }

        public Trader(string firstName, string lastName, int age, double experience, Adress adress, int commision, Efficiency efficiency) : base(firstName, lastName, age, experience, adress)
        {
            this.commision = commision;
            this.efficiency = efficiency;
        }

        public override double getValue() => (int)efficiency * experience;
    }
}
