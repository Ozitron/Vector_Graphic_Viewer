using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.Util;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Shape = System.Windows.Shapes.Shape;

namespace VectorGraphicViewer.Model.Base
{
    internal class PolygonShape : LinearShape
    {
        bool IsFilled { get; }
        
        protected PolygonShape(Point[] points, Color color, bool isFilled) : base(points, color)
        {
            IsFilled = isFilled;
        }

        public override Shape GetRelativeShape(Point canvasCenter, double scaleFactor, SolidColorBrush brush)
        {
            var relativePoints = Points.ToArray();
            relativePoints = GeometryUtil.GetRelativePointArray(relativePoints, canvasCenter, scaleFactor);

            var polygon = new Polygon
            {
                Stroke = brush,
                StrokeThickness = 2
            };

            if (IsFilled)
                polygon.Fill = brush;

            foreach (var point in relativePoints)
            {
                polygon.Points.Add(point);
            }

            return polygon;
        }
    }
}
