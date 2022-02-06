using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    internal class Triangle : PolygonShape
    {
        public Triangle(Point[] points, Color color, bool isFilled) : base(points, color, isFilled)
        {
        }
    }
}
