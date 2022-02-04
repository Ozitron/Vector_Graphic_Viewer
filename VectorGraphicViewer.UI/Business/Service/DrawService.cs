using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Helper;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Business.Service
{
    internal class DrawService : IDrawService
    {

        public List<Shape> GetScaledShapes(List<IShape> shapes, Point canvas)
        {
            var scaledShapes = new List<Shape>();
            var center = FindCartesianCenter(canvas);
            var scaleFactor = 1;//CalculateScaleFactor(canvas, shapes);



            foreach (var shape in shapes)
            {
                if (shape is Model.Line line)
                {
                    line.A = SetPointPosition(line.A, center, scaleFactor);

                    scaledShapes.Add(new Line
                    {
                        // x line
                        Stroke = Brushes.Black,
                        X1 = line.A.X,
                        X2 = line.B.X,
                        Y1 = line.A.Y,
                        Y2 = line.B.Y,
                        StrokeThickness = 2
                    });



                    //_lineShape = (ILine)shape;

                    //_points = _lineShape.Points.ToArray();
                    //_points = DrawHelper.SetPointArrayPosition(_points, _center, _scaleFactor);

                    //DrawLine(_points);
                }
                //if (shape is ILinearShape)
                //{
                //    _linearShape = (ILinearShape)shape;

                //    _points = _linearShape.Points.ToArray();
                //    _points = DrawHelper.SetPointArrayPosition(_points, _center, _scaleFactor);

                //    DrawPolygon(_points);
                //}
                //else if (shape is IEllipticalShape)
                //{
                //    _ellipticalShape = (IEllipticalShape)shape;

                //    var shapeCenter = _ellipticalShape.Center;
                //    shapeCenter = DrawHelper.SetPointPosition(shapeCenter, _center, _scaleFactor);

                //    var radius = _ellipticalShape.Radius;
                //    radius = DrawHelper.SetRadius(radius, _scaleFactor);

                //    DrawCircle(shapeCenter, radius);
                //}
            }




            //// Polygon
            //var p = new Polygon();
            //p.Stroke = Brushes.Black;
            //p.Fill = Brushes.LightBlue;
            //p.StrokeThickness = 2;
            //p.Points = new PointCollection() { new Point(100, 100), new Point(45, 68), new Point(102, 135), new Point(220, 256) };
            //_shapes.Add(p);

            //// Elliptical
            //var ellipse = new EllipseGeometry();
            //ellipse.Center = new Point(150, 150);
            //ellipse.RadiusX = 60;
            //ellipse.RadiusY = 40;
            //var path2 = new Path();
            //path2.Stroke = Brushes.Red;
            //path2.Data = ellipse;
            //_shapes.Add(path2);

            return scaledShapes;
        }


        internal static Point SetPointPosition(Point point, Point center, double scaleFactor)
        {
            point.X = (point.X * scaleFactor) + center.X;
            point.Y = (center.Y) - (point.Y * scaleFactor);

            return point;
        }

        //internal static double CalculateScaleFactor(Point center, IList<IShape> shapeList)
        //{
        //    double xMax = 0;
        //    double yMax = 0;
        //    double xMaxTemp;
        //    double yMaxTemp;

        //    foreach (var shape in shapeList)
        //    {
        //        if (shape is Model.Line)
        //        {
        //            var linearShape = (Model.Line)shape;
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
        //        while (yMax * xMaxTemp > (center.Y * 0.98))
        //        {
        //            xMaxTemp *= 0.99;
        //        }

        //        return xMaxTemp > 1 ? xMaxTemp : 1;
        //    }
        //    else
        //    {
        //        while (xMax * yMaxTemp > center.X * 0.98)
        //        {
        //            yMaxTemp *= 0.99;
        //        }

        //        return yMaxTemp > 1 ? yMaxTemp : 1;
        //    }
        //}

        internal static Point FindCartesianCenter(Point canvas) => new(canvas.X / 2.0, canvas.Y / 2.0);
    }
}
