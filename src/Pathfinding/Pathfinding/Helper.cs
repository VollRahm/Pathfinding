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

    }
}
