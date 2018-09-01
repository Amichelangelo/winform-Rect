using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class Signal
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
    enum Sgl
    {
        RS_POSITIONCHANGE = 1, // 改变位置
        RS_SHOWINFO, // 显示信息
        RS_KILL,// 关闭矩形
        RS_QUIT // 退出
    }
}
