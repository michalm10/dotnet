using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public abstract class Need
    {
        private static int _Id = 0;
        public int Id { get; set; }

        public Need()
        {
            Id = _Id++;
        }

        public abstract (List<Type>, double, int) Accept(NeedVisitor visitor);
    }
}
