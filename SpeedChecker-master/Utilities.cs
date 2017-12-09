using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedChecker
{
    public static class Utilities
    {
        #region Functions
        public static object NullAsObject()
        {
            var x = null as object;
            Thread.Sleep(500);
            return x;
        }
        public static object ObjectNull()
        {
            var x = (object)null;
            Thread.Sleep(500);
            return x;
        }
        public static object SetToNull()
        {
            object x = null;
            Thread.Sleep(500);
            return x;
        }

        public static object ProcessArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                //array[i].ToString();
            }
            return null;
        }

        public static object ProcessListFor<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                //list[i].ToString();
            }
            return null;
        }

        public static object ProcessListForeach<T>(List<T> list)
        {
            foreach(T el in list)
            {
                //el.ToString();
            }

            return null;
        }

        #endregion
    }
}
