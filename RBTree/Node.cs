using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RBTree
{
    public class Node
    {
        public NodeColor colour;
        public Node? Left;
        public Node? Right;
        public Node? Parent;
        public int Value;

        public Node(int data) { this.Value = data; }
        public Node(NodeColor colour) { this.colour = colour; }
        public Node(int data, NodeColor colour) { this.Value = data; this.colour = colour; }
    }
}
