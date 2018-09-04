using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleThread
{
    class DeriveRect:Rect
    {
        public DeriveRect(int id, string text, Size size,Point location):base(id,text,size,location)
        {
        }

        protected override void OnPositionChanged(PositionChangedEventArgs e)
        {
            RectThread.SendSignal(Id,1,e.Size,e.Location);
            base.OnPositionChanged(e);
        }

    }
}
