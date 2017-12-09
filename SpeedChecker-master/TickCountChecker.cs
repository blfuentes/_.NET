using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class TickCountChecker : Checker
    {
        public override object RunTest<T>(Func<T> method)
        {
            int start = Environment.TickCount;
            var result = method();
            int duration = Environment.TickCount - start;
            Console.WriteLine("TickCount measured time: {0} ms", duration);

            return result;
        }
    }
}
