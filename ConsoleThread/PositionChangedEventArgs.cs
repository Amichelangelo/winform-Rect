using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ConsoleThread
{
    public delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs e);
    public class PositionChangedEventArgs:EventArgs
    {
        public Size Size { get; set; }
        public Point Location { get; set; }
        public PositionChangedEventArgs(Size size, Point location)
        {
            Size = size;
            Location = location;
        }

    }
}
