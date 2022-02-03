using System.Drawing;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Triangle : Line
    {
        public Point C { get; }
        public bool IsFilled { get; }

        public Triangle(Point a, Point b, Point c, bool isFilled, Color color) : base(a, b, color)
        {
            C = c;
            IsFilled = isFilled;
        }
    }
}
