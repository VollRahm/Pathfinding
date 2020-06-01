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

        }

        public void ToggleWall(Point pt)
        {
            var x =pt.X - pt.X % NODE_SIZE;
            var y = pt.Y - pt.Y % NODE_SIZE;
            var node = DrawnNodes[new Point(x, y)];
            if (node.Type == NodeType.Open)
            {
                node.SetType(NodeType.Wall);
            }
            else if (node.Type == NodeType.Wall)
            {
                node.SetType(NodeType.Open);
            }
        }
    }
}
