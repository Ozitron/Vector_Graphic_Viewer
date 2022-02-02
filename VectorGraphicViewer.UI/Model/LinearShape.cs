using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model
{
    public abstract class LinearShape : IShape
    {
        public abstract Color Color { get; }
        public abstract Point[] Points { get; set; }

        public Path GetShape(double scale = 1)
        {
            var geometryGroup = new GeometryGroup();

            for (var i = 0; i < Points.Length; i++)
            {
                for (var k = Points.Length - 1; k > 0; k--)
                {
                    if (i == k)
                        break;

                    var line = new LineGeometry
                    {
                        StartPoint = Points[i],
                        EndPoint = Points[k]
                    };
                    geometryGroup.Children.Add(line);
                }
            }

            var path = new Path
            {
                Stroke = Brushes.Black,
                Data = geometryGroup
            };

            return path;
        }
    }
}
