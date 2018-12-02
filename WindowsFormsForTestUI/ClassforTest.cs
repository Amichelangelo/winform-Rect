using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsForTestUI
{
    class ClassforTest
    {
        public ClassforTest()
        {
            var br = new BinaryReader(Stream.Null);
            br.ReadChar();
        }
    }
}
