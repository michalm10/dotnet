using market.Needs;
using System;
using System.Reflection.Metadata.Ecma335;

namespace market
{
    public class NeedVisitorImpl : NeedVisitor
    {
        public (List<Type>, double, int) Visit(Needs.Electronics need) => (need.products, need.frequency, 2);
        public (List<Type>, double, int) Visit(Needs.Clothes need) => (need.products, need.frequency, 3);
        public (List<Type>, double, int) Visit(Needs.Food need) => (need.products, need.frequency, 4);
    }
}