using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsync
{
    class Application
    {
        delegate string DealRequestDelegate(string askCode);

        static void AppMain()
        {
            string askCode;
            int clientId = 0;
            string response;
            var s = new Server();
            DealRequestDelegate drd = s.DealRequest;
            while (true)
            {
                askCode = s.ReceiveRequest(ref clientId);
                drd.BeginInvoke(askCode, Completed, new object[] {drd, clientId, s});
            }
        }

        static void Completed(IAsyncResult iar)
        {
            var objs = (object[])iar.AsyncState;
            var drd = (DealRequestDelegate)objs[0];
            var clientId = (int)objs[0];
            var s = (Server) objs[3];
            string response;
            try
            {
                response = drd.EndInvoke(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            s.SendResponse(response, clientId);
        }
    }
}
