using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    public class RectApplication
    {
        public static event EventHandler ApplicationExit;
        public static event EventHandler ThreadExit;

        /// <summary>
        /// 开启信号循环
        /// </summary>
        /// <param name="rect">主rect</param>
        public static void Start(Rect rect) => RectThread.FromCurrent().StartLoop(1, rect);

        /// <summary>
        /// 执行一次信号循环，进行一次信号处理
        /// </summary>
        public static void DoSignal() => RectThread.FromCurrent().StartLoop(2, null);

        internal static void RaiseThreadExit() => ThreadExit?.Invoke(null,EventArgs.Empty);

        internal static void RaiseApplicationExit() => ApplicationExit?.Invoke(null, EventArgs.Empty);
    }
}
