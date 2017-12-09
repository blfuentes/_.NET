using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTimeChecker dtChecker = new DateTimeChecker();
            ProcessChecker prChecker = new ProcessChecker();
            StopwatchChecker stChecker = new StopwatchChecker();
            TickCountChecker tcChecker = new TickCountChecker();

            IChecker[] theChecker = { dtChecker, prChecker, stChecker, tcChecker };

            int[] array = new int[100000];
            List<int> listfor = new List<int>();
            List<int> listforeach = new List<int>();

            Random random = new Random(10);

            for (int i = 0; i < 100000; i++)
            {
                array[i] = random.Next(0, 100);
                listfor.Add(random.Next(0, 100));
                listforeach.Add(random.Next(0, 100));
            }

            TestClass myClass = new TestClass(100000000);
            Thread.Sleep(10);
            for (int count = 0; count < 5; count++ )
            {
                Console.WriteLine("Results for round {0}", count);
                foreach (IChecker ck in theChecker)
                {
                    //ck.RunTest<object>(() => Utilities.NullAsObject());
                    //ck.RunTest<object>(() => Utilities.ObjectNull());
                    //ck.RunTest<object>(() => Utilities.SetToNull());

                    //ck.RunTest<object>(() => Utilities.ProcessListFor<int>(listfor));
                    //ck.RunTest<object>(() => Utilities.ProcessArray<int>(array));
                    //ck.RunTest<object>(() => Utilities.ProcessListForeach<int>(listforeach));

                    ck.RunTest<object>(() => myClass.LoopWithField());
                    ck.RunTest<object>(() => myClass.LoopWithCopy());
                    ck.RunTest<object>(() => myClass.LoopWithProperty());

                    //Console.WriteLine();
                }
                Console.WriteLine();
            }
                

            Console.ReadLine();
        }



    }
}
