using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class TestAsync
    {
        public static async Task<string> TestMAsync()
        {
            re += @"测试开始：" + DateTime.Now.Millisecond + "------>";

            // 加载新闻
            var s1 = await Task.Run(() => GetNewsInfo());
            Console.WriteLine(s1);
            // 加载用户信息
            var s2 = await Task.Run(() => GetUserInfo());
            Console.WriteLine(s2);
            return re + s1 + "------>" + s2;
        }

        private static string GetNewsInfo()
        {
            var idNew = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(idNew.ToString());
            re += "开始加载新闻：" + DateTime.Now.Millisecond + "------>";
            for (int i = 0; i < 5000000; i++)//耗时操作，模拟加载新闻
            {
                i = i * 2;
                i = i / 2;
            }
            re += "完成新闻加载：" + DateTime.Now.Millisecond + "------>";
            return "新闻加载完成"+ DateTime.Now.Millisecond;
        }

        private static string GetUserInfo()
        {
            var idInfo =  Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(idInfo.ToString());
            re += "开始加载用户信息：" + DateTime.Now.Millisecond + "------>";
            for (int i = 0; i < 5000000; i++)//耗时操作，模拟加载新闻
            {
                i = i * 2;
                i = i / 2;
            }
            re += "完成用户信息加载：" + DateTime.Now.Millisecond + "------>";
            return "用户信息加载完成" + DateTime.Now.Millisecond;
        }

        public static string re;
    }
}
