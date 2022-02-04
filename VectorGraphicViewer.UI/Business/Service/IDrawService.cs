using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Business.Service
{
    internal interface IDrawService
    {
        List<Shape> GetScaledShapes(List<IShape> shapes, Point center);



        //// Polygon
        //var p = new Polygon();
        //p.Stroke = Brushes.Black;
        //p.Fill = Brushes.LightBlue;
        //p.StrokeThickness = 2;
        //p.Points = new PointCollection() { new Point(100, 100), new Point(45, 68), new Point(102, 135), new Point(220, 256) };
        //_scaledShapes.Add(p);

        //// Elliptical
        //var ellipse = new EllipseGeometry();
        //ellipse.Center = new Point(150, 150);
        //ellipse.RadiusX = 60;
        //ellipse.RadiusY = 40;
        //var path2 = new Path();
        //path2.Stroke = Brushes.Red;
        //path2.Data = ellipse;
        //_scaledShapes.Add(path2);
    }
}
