using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    internal class Signal
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public object LeftParam { get; set; }
        public object RightParam { get; set; }

        public Signal(int id, int type, object leftParam, object rightParam)
        {
            Id = id;
            Type = type;
            LeftParam = leftParam;
            RightParam = rightParam;
        }
    }
    internal enum Sgl
    {
        RsPositionchange = 1, // 改变位置
        RsShowinfo, // 显示信息
        RsKill,// 关闭矩形
        RsQuit // 退出
    }
}
