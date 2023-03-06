using System;

namespace market
{
    public interface NeedVisitor
    {
        public (List<Type>, double, int) Visit(Needs.Electronics need);
        public (List<Type>, double, int) Visit(Needs.Clothes need);
        public (List<Type>, double, int) Visit(Needs.Food need);
    }
}