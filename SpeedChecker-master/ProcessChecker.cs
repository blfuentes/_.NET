using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class ProcessChecker : Checker
    {
        public override object RunTest<T>(Func<T> method)
        {
            TimeSpan begin = Process.GetCurrentProcess().TotalProcessorTime;
            var result = method();
            TimeSpan end = Process.GetCurrentProcess().TotalProcessorTime;
            Console.WriteLine("Process.TotalProcessor measured time: {0} ms", (end - begin).TotalMilliseconds);

            return result;
        }
    }
}
