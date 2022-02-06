using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Service
{
    internal interface IDrawService
    {
        Task<List<object>> GetScaledShapes(List<IShape> shapes, Point center);
    }
}
