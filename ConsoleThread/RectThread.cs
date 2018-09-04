using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    internal class RectThread
    {
        private static List<Rect> _rects = new List<Rect>();
        private static Queue<Signal> _signalList = new Queue<Signal>();
        private int _mySignalLoopCount;
        private static int _totalSignalLoopCount;
        private static Dictionary<int, RectThread> _allRectThreads = new Dictionary<int, RectThread>();

        /// <summary>
        /// 获取与当前线程相关的RectThread对象
        /// </summary>
        /// <returns></returns>
        public static RectThread FromCurrent()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            if (_allRectThreads.ContainsKey(id))
            {
                return _allRectThreads[id];
            }

            var thread = new RectThread();
            _allRectThreads.Add(id, thread);
            return _allRectThreads[id];
        }

        /// <summary>
        /// 开始信号循环
        /// </summary>
        /// <param name="reason">1是第一层，2是第二层</param>
        /// <param name="rect"></param>
        public void StartLoop(int reason, Rect rect)
        {
            _mySignalLoopCount++;
            _totalSignalLoopCount++;
            if (reason == 1)
            {
                if (_mySignalLoopCount != 1) return; // 不能嵌套调用第一层循环
                rect.Kill += (o, e) => PostQuit();
            }

            Signal signal;
            while (GetSignal(out signal))
            {
                DispatchSignal(signal);
                if (reason == 2) break;
            }

            _mySignalLoopCount--;
            _totalSignalLoopCount--;
            if (reason == 1) Dispose(); // 退出外层循环
        }

        public void AddRect(Rect rect) => _rects.Add(rect);

        public void Dispose()
        {
            if (_mySignalLoopCount != 0)
                PostQuit();
            else
            {
                RectApplication.RaiseThreadExit();
                if (_totalSignalLoopCount == 0)
                    RectApplication.RaiseApplicationExit();
            }
        }

        public static void Main()
        {
            /*
             * 初始化4个Rect对象，添加到集合中
             * rects.Add(new Rect(1, "my name is Rect1", new Size(100, 100), new Point(10, 10)));
             * rects.Add(new Rect(2, "my name is Rect2", new Size(455, 250), new Point(100, 150)));
             * rects.Add(new Rect(3, "my name is Rect3", new Size(300, 500), new Point(250, 100)));
             * rects.Add(new Rect(4, "my name is Rect4", new Size(300, 600), new Point(50, 80)));
            */
            var chiefRect = new Rect(1, "i am the chiefRect", new Size(100, 100), new Point(10, 10));
            chiefRect.Kill += (o, e) => PostQuit();
            chiefRect.PositionChanged += (o, e) =>
                Console.WriteLine(chiefRect.Id.ToString() + $"号矩形,改变大小为：{e.Size}, 位置为：{e.Location}");

            var deriveRect = new DeriveRect(2, "i am the deriveRect", new Size(150, 150), new Point(100, 100));
            deriveRect.PositionChanged += (o, e) =>
                Console.WriteLine(deriveRect.Id.ToString() + $"号矩形,改变大小为：{e.Size}, 位置为：{e.Location}");
            _rects.Add(chiefRect);
            _rects.Add(deriveRect);

            // 循环接受用户收入
            Signal signal = null;
            while (GetSignal(out signal))
            {
                DispatchSignal(signal);
            }

            Console.WriteLine("the thread exit");
            Console.ReadKey(); //阻塞查看运行情况
        }

        static bool GetSignal(out Signal signal)
        {
            START:
            //var input = Console.ReadLine(); //接受用户输入
            //if (input != null)
            //{
            //    var inputs = 
            //        input.Split(' ');
            //    _signalList.Enqueue(new Signal(int.Parse(inputs[0]), int.Parse(inputs[1]), new Size(int.Parse(inputs[2]), int.Parse(inputs[3])),new Point(int.Parse(inputs[4]), int.Parse(inputs[5]))));
            //}

            if (_signalList.Count != 0) // 需要处理同步线程
            {
                signal = _signalList.Dequeue();
                return signal.Type != (int) Sgl.RsQuit;
            }

            Thread.Sleep(1);
            goto START;
        }

        static void DispatchSignal(Signal signal)
        {
            foreach (var rect in _rects)
            {
                if (rect.Id != signal.Id || !rect.Alive) continue;
                rect.RectProc(signal.Id, signal.Type, signal.LeftParam, signal.RightParam);
                break;
            }
        }

        public static void PostQuit() => _signalList.Enqueue(new Signal(0, (int) Sgl.RsQuit, null, null));

        public static void SendSignal(int id, int type, object leftParam, object rightParam) =>
            _signalList.Enqueue(new Signal(id, type, leftParam, rightParam));

    }
}
