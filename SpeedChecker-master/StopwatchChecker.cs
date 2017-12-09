using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class StopwatchChecker : Checker
    {
        public override object RunTest<T>(Func<T> method)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var result = method();
            watch.Stop();
            Console.WriteLine("Stopwatch measured time: {0} ms", watch.ElapsedMilliseconds);

            return result;
        }
    }
}
