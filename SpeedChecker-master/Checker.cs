using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    public abstract class Checker : IChecker
    {
        public abstract object RunTest<T>(Func<T> method);
    }
}
