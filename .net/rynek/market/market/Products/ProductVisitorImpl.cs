using market;
using System;
using System.Reflection.Metadata.Ecma335;

namespace market
{
    public class ProductVisitorImpl : ProductVisitor
    {
        public double Visit(Grocery product) => product.price + product.tax;
        public double Visit(Jacket product) => product.price + product.tax;
        public double Visit(Smartphone product) => product.price + product.tax;
        public double Visit(Computer product) => product.price + product.tax;
        public double Visit(FoodDelivery product) => product.price + product.tax;
        public double Visit(Shoes product) => product.price + product.tax;
    }
}