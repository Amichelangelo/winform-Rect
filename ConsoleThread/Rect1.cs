using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    partial class Rect1:Rect
    {
        public Rect1(int id, string text, Size size, Point location) : base(id, text, size, location) => Init();

        public Rect1() => Init();

        private void MyChildRect_Kill(object sender, EventArgs e)
        {
            for (var i = 0; i < 10000 * 10000; i++)
            {
                RectApplication.DoSignal();
            }
        }
    }

    partial class Rect1 : Rect
    {
        private Rect _myChildRect;

        private void Init()
        {
            _myChildRect = new Rect();
            _myChildRect.Kill += MyChildRect_Kill;
        }
    }
}
