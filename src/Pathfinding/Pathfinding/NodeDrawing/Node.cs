using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pathfinding.NodeDrawing
{
    public class Node
    {
        public Border Element { get; set; }
        public NodeType Type { get; protected set; }

        public Color Color
        {
            get => ((SolidColorBrush)Element.Background).Color;
            set => Element.Background = new SolidColorBrush(value);
        }

        public Node(double x, double y)
        {
            Element = new Border();
            SetPos(new Point(x, y));
            Element.BorderThickness = new Thickness(1);
            Element.BorderBrush = new SolidColorBrush(Colors.Black);
            Element.Height = Vars.NODE_SIZE;
            Element.Width = Vars.NODE_SIZE;
            SetType(NodeType.Open);
        }

        public void Draw()
        {
            if (!Vars.MainCanv.Children.Contains(Element))
                Vars.MainCanv.Children.Add(Element);
        }

        public void SetPos(Point pt)
        {
            Canvas.SetLeft(Element, pt.X);
            Canvas.SetTop(Element, pt.Y);
        }

        public void Remove()
        {
            if (Vars.MainCanv.Children.Contains(Element))
                Vars.MainCanv.Children.Remove(Element);
        }

        public void SetType(NodeType type)
        {
            Type = type;
            switch (type)
            {
                case NodeType.Open:
                    Color = Colors.LightGreen;
                    break;

                case NodeType.Start:
                    Color = Colors.LightBlue; 
                    break;

                case NodeType.Wall:
                    Color = Colors.Black;
                    break;

                case NodeType.Closed:
                    Color = Colors.Red; 
                    break;

                case NodeType.Finish:
                    Color = Colors.Blue;
                    break;
            }
        }
    }

    public enum NodeType
    {
        Open,
        Closed,
        Start,
        Finish,
        Wall
    }
}
