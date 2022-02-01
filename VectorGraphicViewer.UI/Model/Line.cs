using System;
using System.Drawing;
using System.Windows.Controls;

namespace VectorGraphicViewer.UI.Model
{
    public class Line 
    {
        public Point[] Points { get; set; }

        public Line(Point a, Point b)
        {
            Points = new Point[] { a, b };
        }
    }
}
