using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            var scaleFactor = CalculateScaleFactor(canvas, shapes);

            //GetCartesianPlaneLines();

            // x line
            scaledShapes.Add(new Line
            {
                Stroke = Brushes.Black,
                X1 = canvas.X / 2,
                X2 = canvas.X / 2,
                Y1 = 0,
                Y2 = canvas.Y,
                StrokeThickness = 2
            });

            // y line
            scaledShapes.Add(new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                X2 = canvas.X,
                Y1 = canvas.Y / 2,
                Y2 = canvas.Y / 2,
                StrokeThickness = 2
            });

            double number;
            const double interval = 50.0;
            // Draw X line and print scaled line numbers
            int count = 1;
            while (count * interval < canvas.X / 2.0)
            {
                // draw vertical dotted lines
                scaledShapes.Add(new Line
                {
                    Stroke = Brushes.Black,
                    X1 = center.X - count * interval,
                    Y1 = 0,
                    X2 = center.X - count * interval,
                    Y2 = canvas.Y,
                    StrokeThickness = 0.2
                });

                scaledShapes.Add(new Line
                {
                    Stroke = Brushes.Black,
                    X1 = center.X + count * interval,
                    Y1 = 0,
                    X2 = center.X + count * interval,
                    Y2 = canvas.Y,
                    StrokeThickness = 0.2,
                    
                });
                

                // print line X numbers

                count++;
            }


            if (shapes != null)
            {
                foreach (var shape in shapes)
                {
                    if (shape is Model.Triangle triangle)
                    {
                        triangle.Points = SetPointArrayPosition(triangle.Points, center, scaleFactor);

                        var polygon = new Polygon();
                        polygon.Stroke = Brushes.Black;
                        polygon.StrokeThickness = 2;

                        if (triangle.IsFilled)
                            polygon.Fill = Brushes.Black;

                        foreach (var point in triangle.Points)
                        {
                            polygon.Points.Add(point);
                        }

                        scaledShapes.Add(polygon);
                    }
                    else if (shape is Model.Line line)
                    {
                        line.Points = SetPointArrayPosition(line.Points, center, scaleFactor);

                        scaledShapes.Add(new Line
                        {
                            // x line
                            Stroke = Brushes.Black,
                            X1 = line.Points[0].X,
                            X2 = line.Points[1].X,
                            Y1 = line.Points[0].Y,
                            Y2 = line.Points[1].Y,
                            StrokeThickness = 2
                        });
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

        internal static double CalculateScaleFactor(Point center, IList<IShape> shapeList)
        {
            if (shapeList == null || shapeList.Count == 0)
                return 1;

            double xMax = 0;
            double yMax = 0;
            double xMaxTemp;
            double yMaxTemp;

            foreach (var shape in shapeList)
            {
                if (shape is ILine linearShape)
                {
                    xMaxTemp = linearShape.Points.Select(max => Math.Abs(max.X)).Max();
                    if (xMaxTemp > xMax)
                        xMax = xMaxTemp;

                    yMaxTemp = linearShape.Points.Select(max => Math.Abs(max.Y)).Max();
                    if (yMaxTemp > yMax)
                        yMax = yMaxTemp;
                }
                else if (shape is Model.Ellipse ellipticalShape)
                {
                    if (ellipticalShape.Radius > xMax) xMax = ellipticalShape.Radius;
                    if (ellipticalShape.Radius > yMax) yMax = ellipticalShape.Radius;
                }
            }

            xMaxTemp = (center.X / 2) / xMax;
            yMaxTemp = (center.Y / 2) / yMax;

            if (xMaxTemp > yMaxTemp)
            {
                return yMaxTemp * 0.95;
            }

            return xMaxTemp * 0.95;
        }

        private static Point[] SetPointArrayPosition(Point[] points, Point center, double scaleFactor)
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = SetPointPosition(points[i], center, scaleFactor);
            }

            return points;
        }

        private static Point SetPointPosition(Point point, Point center, double scaleFactor)
        {
            point.X = (point.X * scaleFactor) + center.X;
            point.Y = (center.Y) - (point.Y * scaleFactor);

            return point;
        }

        public static Point FindCartesianCenter(Point canvas) => new(canvas.X / 2.0, canvas.Y / 2.0);



    }
}
