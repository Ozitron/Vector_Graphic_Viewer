using System.Windows.Media;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Shape = System.Windows.Shapes.Shape;

namespace VectorGraphicViewer.Model.Base
{
    public interface IShape
    {
        public Color Color { get; }

        public Shape GetRelativeShape(Point canvasCenter, double scaleFactor, SolidColorBrush brush);
    }
}
