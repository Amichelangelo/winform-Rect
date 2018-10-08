using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsync
{
    class TestInvoke
    {
        delegate string CombineStringDelegate(string strFirst, string strSecond);

        public static void Test()
        {
            var Si = new SomeInvoke();
            CombineStringDelegate cbd = Si.CombineString; 
            cbd.BeginInvoke("hello", "world", Completed, cbd);
        }

        public static void Completed(IAsyncResult iar)
        {
            var del = iar.AsyncState as CombineStringDelegate;
            string result;
            try
            {
                result = del?.EndInvoke(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine(result);
        }
    }
}
