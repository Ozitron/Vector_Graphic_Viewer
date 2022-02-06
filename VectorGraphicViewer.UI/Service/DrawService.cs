using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectorGraphicViewer.UI.Model.Base;
using VectorGraphicViewer.UI.Util;
using Ellipse = VectorGraphicViewer.UI.Model.Ellipse;

namespace VectorGraphicViewer.UI.Service
{
    internal class DrawService : IDrawService
    {
        public Task<List<object>> GetScaledShapes(List<IShape> shapes, Point canvas)
        {
            var scaledShapes = new List<object>();
            var canvasCenter = GeometryUtil.GetCartesianCenter(canvas);
            var scaleFactor = CalculateScaleFactor(canvas, shapes);

            scaledShapes.AddRange(DrawCartesianLines(canvas, canvasCenter, scaleFactor));

            if (shapes != null)
            {
                foreach (var shape in shapes)
                {
                    var brush = new SolidColorBrush(Color.FromArgb(shape.Color.A, shape.Color.R, shape.Color.G, shape.Color.B));
                    
                    scaledShapes.Add(shape.GetRelativeShape(canvasCenter, scaleFactor, brush));
                }
            }

            return Task.FromResult(scaledShapes);
        }

        internal static List<object> DrawCartesianLines(Point canvas, Point center, double scaleFactor)
        {
            var scaledShapes = new List<object>
            {
                // x line
                new System.Windows.Shapes.Line
                {
                    Stroke = Brushes.Black,
                    X1 = canvas.X / 2,
                    X2 = canvas.X / 2,
                    Y1 = 0,
                    Y2 = canvas.Y,
                    StrokeThickness = 2
                },
                // y line
                new System.Windows.Shapes.Line
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
                scaledShapes.Add(new System.Windows.Shapes.Line
                {
                    Stroke = Brushes.Gray,
                    X1 = center.X - count * interval,
                    Y1 = 0,
                    X2 = center.X - count * interval,
                    Y2 = canvas.Y,
                    StrokeThickness = 0.2
                });
                scaledShapes.Add(new System.Windows.Shapes.Line
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
                if (shape is LinearShape linearShape)
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



    }
}
