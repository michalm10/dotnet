using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public abstract class Product
    {
        private static int _Id = 0;
        public int Id { get; set; }

        public Product()
        {
            Id = _Id++;
        }

        public abstract double Accept(ProductVisitor visitor);
    }
}
