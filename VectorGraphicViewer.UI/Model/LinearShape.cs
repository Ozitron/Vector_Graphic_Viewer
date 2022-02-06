using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class LinearShape : ILinearShape
    {
        public Point[] Points { get; set; }
        public Color Color { get; set; }

        public LinearShape(Point a, Point b, Color color)
        {
            Points = new[] { a, b };
            Color = color;
        }
    }
}
