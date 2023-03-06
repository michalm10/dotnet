using System;

namespace market
{
    public interface ProductVisitor
    {
        public double Visit(Grocery product);
        public double Visit(Jacket product);
        public double Visit(Smartphone product);
        public double Visit(Computer product);
        public double Visit(FoodDelivery product);
        public double Visit(Shoes product);
    }
}
