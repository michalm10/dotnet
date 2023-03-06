using System;
using System.Collections.Generic;

namespace RPNCalculator
{
    public abstract class Definitions
    {
        public static Dictionary<string, Func<int, int, int>> _2paramOperations = new Dictionary<string, Func<int, int, int>>
        {
            ["+"] = (fst, snd) => (fst + snd),
            ["-"] = (fst, snd) => (snd - fst),
            ["*"] = (fst, snd) => (fst * snd),
            ["/"] = (fst, snd) => (snd / fst)
        };

        public static Dictionary<string, Func<int, int>> _1paramOperations = new Dictionary<string, Func<int, int>>
        {
            ["!"] = fst =>
            {
                var ret = 1;
                while (fst > 0)
                {
                    ret *= fst;
                    fst--;
                }
                return ret;
            },
            ["||"] = fst => (Math.Abs(fst)),
        };

        public static Dictionary<char, int> baseIndicator = new Dictionary<char, int>();

        static Definitions()
        {
            baseIndicator.Add('b', 2);
            baseIndicator.Add('#', 16);
            baseIndicator.Add('o', 8);
        }
    }

}
