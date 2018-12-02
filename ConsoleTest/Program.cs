using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Test();
            var test = new List<RtspObj>
            {
                new RtspObj(false, "1", "1"),
                new RtspObj(true, "2", "2"),
                new RtspObj(false, "3", "3"),
                new RtspObj(true, "4", "4"),
                new RtspObj(false, "5", "5")
            };

            test.ForEach(item =>
            {
                if (item.IsAdd) return;
                Console.WriteLine(item.RtspName);
                item.IsAdd = true;
            });

            
            //Console.WriteLine("Equals--" + result2);
            Console.ReadLine();
        }

        private static void Test()
        {
            var timer = new Timer(TimerCallBack, null, 10000, 0);
        }

        private static void TimerCallBack(object e)
        {

        }
        public class RtspObj
        {
            /// <summary>
            /// 是否已添加为网络摄像头
            /// </summary>
            public bool IsAdd { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string RtspName { get; set; }

            public string Rtsp { get; set; }

            public RtspObj(bool isAdd, string rtspName, string rtsp)
            {
                IsAdd = isAdd;
                RtspName = rtspName;
                Rtsp = rtsp;
            }
        }
    }
}
