using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corcunrex
{
    public class Node
    {
        public string Value { get; }
        public Node Parent { get; set; }
        public List<Node> Children { get; }

        public Node(string value)
        {
            this.Value = value;
            this.Children = new List<Node>();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
