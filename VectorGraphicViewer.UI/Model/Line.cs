using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Line : ILine
    {
        public Point[] Points { get; set; }
        public Color Color { get; set; }

        public Line(Point a, Point b, Color color)
        {
            Points = new Point[2] { a, b };
            Color = color;
        }
    }
}
