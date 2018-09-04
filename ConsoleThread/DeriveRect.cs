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
        public override void RectProc(int id,int type,object leftParam,object rightParam)
        {
            base.RectProc(id, type, leftParam, rightParam);
        }
        protected override void OnPositionChanged(PositionChangedEventArgs e)
        {
            RectThread.SendSignal(Id,1,e.Size,e.Location);
            base.OnPositionChanged(e);
        }
    }
}
