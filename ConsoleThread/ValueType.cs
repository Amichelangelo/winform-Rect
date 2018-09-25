using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class RefType : ICloneable
    {
        int _a;
        int[] _ref;

        public RefType(int a, int[] @ref)
        {
            _a = a;
            _ref = @ref;
        }
        public object Clone() => new RefType(_a, _ref);
    }



}
