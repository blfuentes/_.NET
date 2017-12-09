using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class DateTimeChecker : Checker
    {
        public override object RunTest<T>(Func<T> method)
        {
            DateTime begin = DateTime.UtcNow;
            var result = method();
            DateTime end = DateTime.UtcNow;
            Console.WriteLine("DateTime.UtcNow measured time: {0} ms", (end - begin).TotalMilliseconds);

            return result;
        }
    }
}
