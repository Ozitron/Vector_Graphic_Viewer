using System.Drawing;
using VectorGraphicViewer.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.Model
{
    internal class Triangle : PolygonShape
    {
        public Triangle(Point[] points, Color color, bool isFilled) : base(points, color, isFilled)
        {
        }
    }
}
