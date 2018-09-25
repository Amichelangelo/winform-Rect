using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleThread
{
    class Node
    {
        public string Name { get; set; }
        public Node Next { get; set; }

        public Node(string name)
        {
            Name = name;
        }

        public int GetNodesCount()
        {
            var count = 0;
            var tmp = this;
            do
            {
                count++;
                tmp = tmp.Next;
            } while (tmp != null);
            return count;
        }

        public Node[] GetNodeList()
        {
            var nodes = new Node[GetNodesCount()];
            var index = 0;
            var tmp = this;
            do
            {
                nodes[index++] = tmp;
                tmp = tmp.Next;
            } while (tmp != null);
            return nodes;
        }
    }
}
