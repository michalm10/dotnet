using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Adress
    {
        public String street { get; set; }
        public String city { get; set; }
        public String buildingNumber { get; set; }
        public String placeNumber { get; set; }

        public Adress(string street, string city, string buildingNumber, string placeNumber)
        {
            this.street = street;
            this.city = city;
            this.buildingNumber = buildingNumber;
            this.placeNumber = placeNumber;
        }
    }
}
