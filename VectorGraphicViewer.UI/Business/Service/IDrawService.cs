using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using VectorGraphicViewer.Model.Base;

namespace VectorGraphicViewer.Business.Service
{
    internal interface IDrawService
    {
        Task<List<object>> GetScaledShapes(List<IShape> shapes, Point center);
    }
}
