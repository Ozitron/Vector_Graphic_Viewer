using System.Collections.Generic;
using System.Windows;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Service
{
    internal interface IDrawService
    {
        List<object> GetScaledShapes(List<IShape> shapes, Point center);
    }
}
