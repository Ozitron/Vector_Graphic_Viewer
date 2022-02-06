using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Triangle : ILine
    {

        public Color Color { get; }
        public bool IsFilled { get; }

        public Point[] Points { get; set; }

        public Triangle(Point a, Point b, Point c, bool isFilled, Color color)
        {
            Points = new[] { a, b, c };
            IsFilled = isFilled;
            Color = color;
        }

    }
}
