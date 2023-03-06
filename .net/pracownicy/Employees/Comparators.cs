using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public abstract class Comparators
    {
        public static Comparison<KeyValuePair<string, Employee>> CompareByExpDesc = 
            (e1, e2) => e1.Value.experience - e2.Value.experience > 0 ? -1 : e1.Value.experience - e2.Value.experience == 0 ? 0 : 1;
        
        public static Comparison<KeyValuePair<string, Employee>> CompareByAgeAsc =
            (e1, e2) => e1.Value.age - e2.Value.age;

        public static Comparison<KeyValuePair<string, Employee>> CompareByLastNameAsc =
            (e1, e2) => e1.Value.lastName.CompareTo(e2.Value.lastName);
    }
}
