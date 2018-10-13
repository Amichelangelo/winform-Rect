using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var test = new int[]{1,2,3,4,5};
            int a1 = 0, a2 = 0;
            Console.WriteLine("a1++"+test[a1++]+"++a2"+test[++a2]);
            Console.ReadLine();
            //RectApplication.ApplicationExit += RectApplication_ApplicationExit;
            //RectApplication.ThreadExit += RectApplication_ThreadExit;
            //RectApplication.Start(new Rect1());
        }

        private static void RectApplication_ThreadExit(object sender, EventArgs e)
        {
            
        }

        static void RectApplication_ApplicationExit(object sender ,EventArgs e)
        { }
    }
}
