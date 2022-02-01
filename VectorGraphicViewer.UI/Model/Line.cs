using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VectorGraphicViewer.UI.Model
{
    public class Line
    {
        public Color Color { get; }

        public Point[] Points { get; }

        public Line(Point pointA, Point pointB, Color color)
        {
            Points = new[] { pointA, pointB };
            Color = color;
        }

        public void Draw(Canvas canvas)
        {

        }
    }
}
