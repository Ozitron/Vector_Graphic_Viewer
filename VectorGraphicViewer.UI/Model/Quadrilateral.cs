using System.Drawing;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Quadrilateral : LinearShape
    {
        public override Color Color { get; }
        public override Point[] Points { get; set; }

        public Quadrilateral(Point a, Point b, Point c, Point d, Color color)
        {
            Points = new Point[] { a, b, c, d };
            Color = color;
        }
    }
}
