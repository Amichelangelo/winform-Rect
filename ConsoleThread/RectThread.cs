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

            while (   GetSignal(out var signal))
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
