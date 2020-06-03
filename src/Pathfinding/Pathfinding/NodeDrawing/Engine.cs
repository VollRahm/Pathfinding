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

        public void Clear()
        {
            DrawnNodes.Values.ToList().ForEach(x => { if (x != startNode && x != endNode && x.Type != NodeType.Wall) x.SetType(NodeType.Open); });
        }

        public async Task FindPath()
        {
            Clear();
            Node currentNode = startNode;
            var passedNodes = new List<Node>();
   
            while (true)
            {
                passedNodes.AddRange(GetSurroundingNodes(currentNode));
                passedNodes = passedNodes.OrderBy(x => x.FCost).ToList();
                currentNode = GetFirstAvailableNode(passedNodes, currentNode);
                if (currentNode == null) break;
                if (currentNode == endNode) break;
                currentNode.SetType(NodeType.Closed);
                await Task.Delay(10);
            }
            if(currentNode == endNode)
            {
                MessageBox.Show("Found path!");
            }
            else
            {
                MessageBox.Show("No path found :(");
            }
        }

        private Node GetFirstAvailableNode(List<Node> nodes, Node excludedNode)
        {
            return nodes.Where(x => x != excludedNode && x.Type != NodeType.Closed).FirstOrDefault();
        }

        private List<Node> GetSurroundingNodes(Node node)
        {
            var nodes = new List<Node>();
            var directions = Enum.GetValues(typeof(Direction));
            foreach (Direction dir in directions)
            {
                
                var nextNode = GetNextNode(node, dir);
                if (nextNode == startNode) continue;
                if (nextNode == null) continue;
                if (nextNode.Type == NodeType.Closed) continue;
                if (nextNode.Type == NodeType.Wall) continue;
                nextNode.FCost = GetNodeFCost(nextNode);
                nodes.Add(nextNode);
            }
            Console.WriteLine();
            return nodes;
        }

        private double GetNodeFCost(Node node)
        {
            var gcost = Helper.Pythag(node.Position, startNode.Position);
            var hcost = Helper.Pythag(node.Position, endNode.Position);
            Console.WriteLine($"GCost: {Math.Round(gcost, 2).ToString("#.00")} | HCost: {Math.Round(hcost,2)} | FCost: {Math.Round(gcost + hcost, 2)}");
            return gcost + hcost;
        }

        private Node GetNextNode(Node node, Direction dir)
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

        private Node GetNode(Point pt)
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
