using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    public interface IChecker
    {
        object RunTest<T>(Func<T> method);
    }
}
