using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public class Shoes : Product
    {
        public string name { get; set; }
        public double price { get; set; }
        public double tax { get; set; }

        public Shoes(string name, double price, double tax) : base()
        {
            this.name = name;
            this.price = price;
            this.tax = tax;
        }

        public override double Accept(ProductVisitor visitor) { return visitor.Visit(this); }
        public override string ToString()
        {
            return $"id: {Id}, name: {name}, price: {price}";
        }
    }
}

