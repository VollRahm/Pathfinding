using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Pathfinding.Vars;

namespace Pathfinding
{
    public static class Helper
    {
        public static Point GridToCoords(Point pt)
        {
            return new Point(pt.X / NODE_SIZE, pt.Y / NODE_SIZE);
        }

        public static double Pythag(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) +
                       Math.Pow(p1.Y - p2.Y, 2));
        }

    }
}
