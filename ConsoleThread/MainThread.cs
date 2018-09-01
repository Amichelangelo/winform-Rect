using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    static class MainThread
    {
        static List<Rect> rects = new List<Rect>();
        static Queue<Signal> signalList = new Queue<Signal>();
        public static void Main()
        {
            /*
             * 初始化4个Rect对象，添加到集合中
             * rects.Add(new Rect(1, "my name is Rect1", new Size(100, 100), new Point(10, 10)));
             * rects.Add(new Rect(2, "my name is Rect2", new Size(455, 250), new Point(100, 150)));
             * rects.Add(new Rect(3, "my name is Rect3", new Size(300, 500), new Point(250, 100)));
             * rects.Add(new Rect(4, "my name is Rect4", new Size(300, 600), new Point(50, 80)));
            */
            var chiefRect = new Rect(1,"i am the chiefRect" , new Size(100,100),new Point(10,10));
            chiefRect.Kill += (o, e) => PostQuit();
            chiefRect.PositionChanged +=(o,e)=> Console.WriteLine(chiefRect.Id.ToString() + $"号矩形,改变大小为：{e.Size}, 位置为：{e.Location}");

            var deriveRect = new DeriveRect(2, "i am the deriveRect", new Size(150, 150), new Point(100, 100));
            deriveRect.PositionChanged += (o, e) => Console.WriteLine(deriveRect.Id.ToString() + $"号矩形,改变大小为：{e.Size}, 位置为：{e.Location}");
            rects.Add(chiefRect);
            rects.Add(deriveRect);

            // 循环接受用户收入
            Signal signal = null;
            while(GetSignal(out signal))
            {
                DispatchSignal(signal);
            }
            Console.WriteLine("the thread exit");
            Console.ReadKey(); //阻塞查看运行情况
        }
        static bool GetSignal(out Signal signal)
        {
            START:
            String input = Console.ReadLine(); //接受用户输入
            String[] inputs = input.Split(' ');
            signalList.Enqueue(new Signal(int.Parse(inputs[0]), int.Parse(inputs[1]), new Size(int.Parse(inputs[2]??null), int.Parse(inputs[3])),new Point(int.Parse(inputs[4]), int.Parse(inputs[5]))));
            if (signalList.Count != 0)
            {
                signal = signalList.Dequeue() as Signal;
                if (signal.Type != (int)Sgl.RS_QUIT)
                    return true;
                else
                    return false;
            }
            else
            {
                Thread.Sleep(1);
                goto START;
            }            
        }
        static void DispatchSignal(Signal signal)
        {
            foreach(var rect in rects)
            {
                if(rect.Id == signal.Id && rect.Alive)
                {
                    rect.RectProc(signal.Id, signal.Type, signal.LeftParam, signal.RightParam);
                    break;
                }
            }
        }
        public static void PostQuit() => signalList.Enqueue(new Signal(0, (int)Sgl.RS_QUIT, null, null));

        public static void SendSignal(int id, int type,object leftParam,object rightParam )=> signalList.Enqueue(new Signal(id, type, leftParam, rightParam));

    }
}
