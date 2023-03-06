using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market.Needs
{
    public class Clothes : Need
    {
        public string name { get; set; }
        public List<Type> products { get; set; } = new List<Type>();
        public double frequency { get; set; }

        public Clothes(string name, List<Type> products, double frequency) : base()
        {
            this.name = name;
            this.products = products;
            this.frequency = frequency;
        }

        public override (List<Type>, double, int) Accept(NeedVisitor visitor) { return visitor.Visit(this); }

        public override string ToString()
        {
            return $"id: {Id}, name: {name}, frequency: {frequency}";
        }
    }
}