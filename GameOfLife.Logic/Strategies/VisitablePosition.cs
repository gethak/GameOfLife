using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Logic
{
    public class VisitablePoint
    {
        private Point _Point;
        private bool _isVisited;

        public bool IsVisited { get => _isVisited; set => _isVisited = value; }
        public Point Point { get => _Point; }
        //For easy access:
        public int X { get => Point.X; }
        public int Y { get => Point.Y; }

        public VisitablePoint(Point Point)
        {
            _Point = Point;
        }
    }
}
