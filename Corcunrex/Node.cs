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
        public Node Parent { get; }
        public List<Node> Children { get; }

        public Node(string value, Node parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<Node>();
        }
    }
}
