using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class InterfaceDispose:IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            if (Disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
            Disposed = true;
        }

        void IDisposable.Dispose()
        {

        }
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
