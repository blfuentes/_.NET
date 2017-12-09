using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class TestClass
    {
        int NumRep { get; set; }
        
        int numRep;
        public TestClass()
        {

        }
        public TestClass(int _numRep)
        {
            numRep = _numRep;
            NumRep = _numRep;
        }

        public object LoopWithField()
        {
            int dummy = 310;
            for (int i = 0; i < numRep; i++)
            {
                dummy++;
            }

            return null;
        }

        public object LoopWithCopy()
        {
            int dummy = 310;
            int numRepCopy = NumRep;
            for (int i = 0; i < numRepCopy; i++)
            {
                dummy++;
            }

            return null;
        }

        public object LoopWithProperty()
        {
            int dummy = 310;
            for (int i = 0; i < NumRep; i++)
            {
                dummy++;
            }

            return null;
        }

    }
}
