using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleThread
{
    public class Rect
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
            RectThread.FromCurrent().AddRect(this);
            Console.WriteLine(Id + "号矩形已创建");
        }

        public Rect()
        {
            Id = int.Parse(DateTime.Today.ToString("yyddmmss"));
            Text = string.Empty;
            Size = new Size(100, 100);
            Location = new Point(100, 100);
            Alive = true;
            RectThread.FromCurrent().AddRect(this);
        }
        /// <summary>
        /// 矩形对外唯一接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="leftParam"></param>
        /// <param name="rightParam"></param>
        public virtual void RectProc (int id, int type, object leftParam,object rightParam)
        {
            switch(type)
            {
                case 1: // 移动位置,改变大小
                    Size = (Size)leftParam;
                    Location = (Point)rightParam;
                    OnPositionChanged(new PositionChangedEventArgs(Size, Location));
                    break;
                case 2: // 显示信息
                    Console.WriteLine(Id.ToString() + $"号矩形大小为：{Size}, 位置为：{Location}");
                    break;
                case 3: // 关闭
                    Alive = false;
                    OnKill(new EventArgs());
                    Console.WriteLine(Id.ToString() + "号矩形已关闭");
                    break;
                default:
                    Console.WriteLine(Id.ToString());
                    break;
            }
                

        }
        protected virtual void OnPositionChanged(PositionChangedEventArgs e) => PositionChanged?.Invoke(this, e);
        protected virtual void OnKill(EventArgs e) => Kill?.Invoke(this, e);
        public event PositionChangedEventHandler PositionChanged;
        public event EventHandler Kill;
    }
}
