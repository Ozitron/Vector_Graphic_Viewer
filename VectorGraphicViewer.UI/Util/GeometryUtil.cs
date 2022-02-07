using System.Windows;

namespace VectorGraphicViewer.Util
{
    internal static class GeometryUtil
    {
        internal static Point[] GetRelativePointArray(Point[] points, Point center, double scaleFactor)
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = GetRelativePoint(points[i], center, scaleFactor);
            }

            return points;
        }

        internal static Point GetRelativePoint(Point point, Point center, double scaleFactor)
        {
            point.X = (point.X * scaleFactor) + center.X;
            point.Y = (center.Y) - (point.Y * scaleFactor);

            return point;
        }

        internal static Point GetCartesianCenter(Point canvas) => new(canvas.X / 2.0, canvas.Y / 2.0);

        internal static double GetRadius(double radius, double scaleFactor) => radius * scaleFactor;
    }
}
