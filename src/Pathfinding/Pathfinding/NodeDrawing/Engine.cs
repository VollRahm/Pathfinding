using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Pathfinding.Vars;

namespace Pathfinding.NodeDrawing
{
    public class Engine
    {
        public Dictionary<Point, Node> DrawnNodes = new Dictionary<Point, Node>();
        private Node startNode;
        private Node endNode;

        public void DrawNodes(Point start, Point end)
        {
            var size = MainCanv.RenderSize;
            for (double x = 0; x < size.Width; x += NODE_SIZE)
            {
                for (double y = 0; y < size.Height; y += NODE_SIZE)
                {
                    var node = new Node(x, y);
                    if (y / NODE_SIZE == start.Y && x / NODE_SIZE == start.X)
                    {
                        node.SetType(NodeType.Start);
                        startNode = node;
                    }
                    if (y / NODE_SIZE == end.Y && x / NODE_SIZE == end.X)
                    {
                        node.SetType(NodeType.Finish);
                        endNode = node;
                    }
                    node.Draw();
                    DrawnNodes.Add(new Point(x, y), node);
                }
            }
        }

        public async Task FindPath()
        {
            Node currentNode = startNode;
            var values = Enum.GetValues(typeof(Direction));
            foreach(Direction dir in values)
            {
                var node = GetNextNode(currentNode, dir);
                node.SetType(NodeType.Closed);
            }
        }

        public Node GetNextNode(Node node, Direction dir)
        {
            var x = node.Position.X;
            var y = node.Position.Y;
            var nodeSize = NODE_SIZE;
            Point nodePt = new Point(-1, -1);
            switch (dir)
            {
                case Direction.Down:
                    nodePt = new Point(x, y + nodeSize);
                    break;

                case Direction.Up:
                    nodePt = new Point(x, y - nodeSize);
                    break;

                case Direction.Left:
                    nodePt = new Point(x - nodeSize, y);
                    break;

                case Direction.Right:
                    nodePt = new Point(x + nodeSize, y);
                    break;

                case Direction.RUp:
                    nodePt = new Point(x + nodeSize, y - nodeSize);
                    break;

                case Direction.RDown:
                    nodePt = new Point(x + nodeSize, y + nodeSize);
                    break;

                case Direction.LUp:
                    nodePt = new Point(x - nodeSize, y - nodeSize );
                    break;

                case Direction.LDown:
                    nodePt = new Point(x - nodeSize, y + nodeSize);
                    break;
            }
            if (nodePt.X == -1) return null;
            return GetNode(nodePt);
        }

        public void ToggleWall(Point pt)
        {
            var node = GetNode(pt);
            if (node.Type == NodeType.Open)
            {
                node.SetType(NodeType.Wall);
            }
            else if (node.Type == NodeType.Wall)
            {
                node.SetType(NodeType.Open);
            }
        }

        public Node GetNode(Point pt)
        {
            var x = pt.X - pt.X % NODE_SIZE;
            var y = pt.Y - pt.Y % NODE_SIZE;
            try
            {
                var node = DrawnNodes[new Point(x, y)];
                return node;
            }
            catch { return null; }
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        RUp,
        RDown,
        LUp,
        LDown
    }
}
