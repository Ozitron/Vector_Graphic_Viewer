using System.Drawing;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    public class Ellipse : IShape
    {
        public Point Center { get; }
        public double Radius { get; }
        public bool IsFilled { get; }
        public Color Color { get; }

        public Ellipse(Point center, double radius, bool isFilled, Color color)
        {
            Center = center;
            Radius = radius;
            IsFilled = isFilled;
            Color = color;
        }
    }
}
