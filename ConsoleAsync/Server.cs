using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsync
{
    class Server
    {
        /// <summary>
        /// return askCode
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public string ReceiveRequest(ref int clientId)
        {
            return clientId.ToString();
        }

        public string DealRequest(string askCode)
        {
            return askCode;
        }
        public void SendResponse(string response, int clientId)
        { }
    }
}
