using System.Collections.Generic;

namespace Threats.Models
{
    public abstract class Node
    {
        private readonly List<Node> children = new();

        public Node(List<Node> children)
        {
            this.children = children;
        }

        public bool Visited { get; protected set; }
    }
}
