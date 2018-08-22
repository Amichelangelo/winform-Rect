using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleThread
{
    class Rect
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public bool Alive { get; set; }
        public Rect(int id,string text,Size size,Point location)
        {
            Id = id;
            Text = text;
            Size = size;
            Location = location;
            Alive = true;
            Console.WriteLine(Id.ToString() + "号矩形已创建");
        }
        //唯一方法：矩形过程
        public void RectProc (int id, int type, object leftParam,object rightParam)
        {
            switch(type)
            {
                case 1:
                    Size = (Size)leftParam;
                    Location = (Point)rightParam;
                    Console.WriteLine(Id.ToString() + $"号矩形,改变大小为：{Size}, 位置为：{Location}");
                    break;
                case 2:
                    Console.WriteLine(Id.ToString() + $"号矩形大小为：{Size}, 位置为：{Location}");
                    break;
                case 3:
                    Alive = false;
                    Console.WriteLine(Id.ToString() + "号矩形已关闭");
                    break;
                default:
                    Console.WriteLine(Id.ToString());
                    break;
            }
                

        }
    }
}
