using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ConcreteEmployees
{
    public class ManualWorker : Employee
    {
        private int _strength;
        public int strength
        {
            get => _strength;
            set
            {
                if (value < 1) _strength = 1;
                else if (value > 100) _strength = 100;
                else _strength = value;
            }
        }
        public ManualWorker(string firstName, string lastName, int age, double experience, Adress adress, int strength) : base(firstName, lastName, age, experience, adress)
        {
            this.strength = strength;
        }

        public override double getValue() => strength * experience / age;
    }
}
