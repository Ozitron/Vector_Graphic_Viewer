using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VectorGraphicViewer.UI.Helper
{
    internal static class DrawHelper
    {
        internal static Point FindCartesianCenter(Point canvas) => new(canvas.X / 2.0, canvas.Y / 2.0);

        //internal static PointF[] SetPointArrayPosition(PointF[] points, PointF center, float scaleFactor)
        //{
        //    for (var i = 0; i < points.Length; i++)
        //    {
        //        points[i] = SetPointPosition(points[i], center, scaleFactor);
        //    }

        //    return points;
        //}



        //internal static float SetRadius(float radius, float scaleFactor) => radius * scaleFactor;

        //internal static float CalculateScaleFactor(PointF center, IList<IShape> shapeList)
        //{
        //    float xMax = 0;
        //    float yMax = 0;
        //    float xMaxTemp;
        //    float yMaxTemp;

        //    foreach (var shape in shapeList)
        //    {
        //        if (shape is ILinearShape)
        //        {
        //            var linearShape = (ILinearShape)shape;
        //            xMaxTemp = linearShape.Points.Select(max => Math.Abs(max.X)).Max();
        //            if (xMaxTemp > xMax)
        //                xMax = xMaxTemp;

        //            yMaxTemp = linearShape.Points.Select(max => Math.Abs(max.Y)).Max();
        //            if (yMaxTemp > yMax)
        //                yMax = yMaxTemp;
        //        }
        //        else if (shape is IEllipticalShape)
        //        {
        //            var ellipticalShape = (IEllipticalShape)shape;

        //            if (ellipticalShape.Radius > xMax) xMax = ellipticalShape.Radius;
        //            if (ellipticalShape.Radius > yMax) yMax = ellipticalShape.Radius;
        //        }
        //    }

        //    xMaxTemp = (center.X / 2) / xMax;
        //    yMaxTemp = (center.Y / 2) / yMax;

        //    if (xMaxTemp > yMaxTemp)
        //    {
        //        while (yMax * xMaxTemp > (center.Y * 0.98f))
        //        {
        //            xMaxTemp *= 0.99f;
        //        }

        //        return xMaxTemp > 1 ? xMaxTemp : 1;
        //    }
        //    else
        //    {
        //        while (xMax * yMaxTemp > center.X * 0.98f)
        //        {
        //            yMaxTemp *= 0.99f;
        //        }

        //        return yMaxTemp > 1 ? yMaxTemp : 1;
        //    }
        //}
    }
}
