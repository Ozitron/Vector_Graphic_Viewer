using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Line : IShape
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Color Color { get; set; }

        public Line(Point a, Point b, Color color)
        {
            A = a;
            B = b;
            Color = color;
        }
    }
}
