using System.Linq;
using System.Windows.Media;
using VectorGraphicViewer.Model.Base;
using VectorGraphicViewer.Util;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Shape = System.Windows.Shapes.Shape;

namespace VectorGraphicViewer.Model
{
    internal class Line : LinearShape
    {
        public Line(Point[] points, Color color) : base(points, color)
        {
        }

        public override Shape GetRelativeShape(Point canvasCenter, double scaleFactor, SolidColorBrush brush)
        {
            var relativePoints = Points.ToArray();
            relativePoints = GeometryUtil.GetRelativePointArray(relativePoints, canvasCenter, scaleFactor);

            return new System.Windows.Shapes.Line
            {
                Stroke = brush,
                X1 = relativePoints[0].X,
                X2 = relativePoints[1].X,
                Y1 = relativePoints[0].Y,
                Y2 = relativePoints[1].Y,
                StrokeThickness = 2
            };
        }
    }
}
