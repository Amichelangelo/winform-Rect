using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsync
{
    class SomeInvoke
    {
        public string CombineString(string strFirst, string strSecond)
        {
            System.Threading.Thread.Sleep(10000);
            return strFirst + "-" + strSecond;
        }
    }

}
