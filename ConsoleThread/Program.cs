using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class Program
    {
        static void Main(string[] args)
        {
            RectApplication.ApplicationExit += RectApplication_ApplicationExit;
            RectApplication.ThreadExit += RectApplication_ThreadExit;
            RectApplication.Start(new Rect1());
        }

        private static void RectApplication_ThreadExit(object sender, EventArgs e)
        {
            
        }

        static void RectApplication_ApplicationExit(object sender ,EventArgs e)
        { }
    }
}
