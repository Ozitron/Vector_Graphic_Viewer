using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Model;
using VectorGraphicViewer.UI.Model.Base;
using Ellipse = VectorGraphicViewer.UI.Model.Ellipse;

namespace VectorGraphicViewer.UI.Business.Service
{
    internal class DrawService : IDrawService
    {
        public List<object> GetScaledShapes(List<IShape> shapes, Point canvas)
        {
            var scaledShapes = new List<object>();
            var center = FindCartesianCenter(canvas);
            var scaleFactor = CalculateScaleFactor(canvas, shapes);

            scaledShapes.AddRange(DrawCartesianLines(canvas, center, scaleFactor));

            if (shapes != null)
            {
                foreach (var shape in shapes)
                {
                    var brush = new SolidColorBrush(Color.FromArgb(shape.Color.A, shape.Color.R, shape.Color.G, shape.Color.B));

                    if (shape is Triangle triangle)
                    {
                        var points = triangle.Points.ToArray();
                        points = SetPointArrayPosition(points, center, scaleFactor);

                        var polygon = new Polygon();
                        polygon.Stroke = brush;
                        polygon.StrokeThickness = 2;

                        if (triangle.IsFilled)
                            polygon.Fill = brush;

                        foreach (var point in points)
                        {
                            polygon.Points.Add(point);
                        }

                        scaledShapes.Add(polygon);
                    }
                    else if (shape is Ellipse ellipseModel)
                    {
                        var scaledRadius = SetRadius(ellipseModel.Radius, scaleFactor);
                        var ellipse = new EllipseGeometry
                        {
                            Center = SetPointPosition(ellipseModel.Center, center, scaleFactor),
                            RadiusX = scaledRadius,
                            RadiusY = scaledRadius
                        };
                        var path = new Path
                        {
                            Stroke = brush,
                            Data = ellipse
                        };
                        scaledShapes.Add(path);
                    }
                    else if (shape is LinearShape line)
                    {
                        var points = line.Points.ToArray();
                        points = SetPointArrayPosition(points, center, scaleFactor);

                        scaledShapes.Add(new Line
                        {
                            Stroke = brush,
                            X1 = points[0].X,
                            X2 = points[1].X,
                            Y1 = points[0].Y,
                            Y2 = points[1].Y,
                            StrokeThickness = 2
                        });
                    }
                }
            }

            return scaledShapes;
        }

        internal static List<object> DrawCartesianLines(Point canvas, Point center, double scaleFactor)
        {
            var scaledShapes = new List<object>
            {
                // x line
                new Line
                {
                    Stroke = Brushes.Black,
                    X1 = canvas.X / 2,
                    X2 = canvas.X / 2,
                    Y1 = 0,
                    Y2 = canvas.Y,
                    StrokeThickness = 2
                },
                // y line
                new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 0,
                    X2 = canvas.X,
                    Y1 = canvas.Y / 2,
                    Y2 = canvas.Y / 2,
                    StrokeThickness = 2
                }
            };

            double number;
            const double interval = 50.0;
            // Draw X line and print scaled line numbers
            var count = 1;
            while (count * interval < canvas.X / 2.0)
            {
                // draw vertical dotted lines
                scaledShapes.Add(new Line
                {
                    Stroke = Brushes.Gray,
                    X1 = center.X - count * interval,
                    Y1 = 0,
                    X2 = center.X - count * interval,
                    Y2 = canvas.Y,
                    StrokeThickness = 0.2
                });
                scaledShapes.Add(new Line
                {
                    Stroke = Brushes.Gray,
                    X1 = center.X + count * interval,
                    Y1 = 0,
                    X2 = center.X + count * interval,
                    Y2 = canvas.Y,
                    StrokeThickness = 0.2,

                });

                // print line X numbers
                number = (count * interval / scaleFactor);
                scaledShapes.Add(new Label
                {
                    Content = $"{number:0.0}",
                    Margin = new Thickness((center.X + (count * interval)) - 15, center.Y, 0, 0)
                });
                scaledShapes.Add(new Label
                {
                    Content = $"{number:0.0}",
                    Margin = new Thickness((center.X - (count * interval)) - 15, center.Y, 0, 0)
                });

                count++;
            }

            // Draw Y line and print scaled line numbers
            count = 1;
            while (count * interval < canvas.Y / 2.0)
            {
                // draw vertical dotted lines
                scaledShapes.Add(new System.Windows.Shapes.Line
                {
                    Stroke = Brushes.Gray,
                    X1 = 0,
                    Y1 = center.Y - (count * interval),
                    X2 = canvas.X,
                    Y2 = center.Y - (count * interval),
                    StrokeThickness = 0.2
                });
                scaledShapes.Add(new System.Windows.Shapes.Line
                {
                    Stroke = Brushes.Gray,
                    X1 = 0,
                    Y1 = center.Y + (count * interval),
                    X2 = canvas.X,
                    Y2 = center.Y + (count * interval),
                    StrokeThickness = 0.2,

                });

                // print line Y numbers
                number = (count * interval / scaleFactor);
                scaledShapes.Add(new Label
                {
                    Content = $"{number:0.0}",
                    Margin = new Thickness(center.X, center.Y - (count * interval) - 12, 0, 0)
                });
                scaledShapes.Add(new Label
                {
                    Content = $"{number:0.0}",
                    Margin = new Thickness(center.X, center.Y + (count * interval) - 12, 0, 0)
                });

                count++;
            }

            return scaledShapes;
        }

        internal static double CalculateScaleFactor(Point center, IList<IShape> shapeList)
        {
            if (shapeList == null || shapeList.Count == 0)
                return 50;

            double xMax = 0;
            double yMax = 0;
            double xMaxTemp;
            double yMaxTemp;

            foreach (var shape in shapeList)
            {
                if (shape is ILinearShape linearShape)
                {
                    xMaxTemp = linearShape.Points.Select(max => Math.Abs(max.X)).Max();
                    if (xMaxTemp > xMax)
                        xMax = xMaxTemp;

                    yMaxTemp = linearShape.Points.Select(max => Math.Abs(max.Y)).Max();
                    if (yMaxTemp > yMax)
                        yMax = yMaxTemp;
                }
                else if (shape is Ellipse ellipticalShape)
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

        internal static double SetRadius(double radius, double scaleFactor) => radius * scaleFactor;

    }
}
