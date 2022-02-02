using System.Drawing;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Triangle : LinearShape
    {
        public override Color Color { get; }
        public sealed override Point[] Points { get; set; }

        public Triangle(Point a, Point b, Point c, Color color)
        {
            Color = color;
            Points = new[] { a, b, c };
        }
    }
}
