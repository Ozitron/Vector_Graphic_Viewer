using System.Collections.Generic;
using VectorGraphicViewer.UI.Model;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Business.Service
{
    internal interface IReadService
    {
        internal IEnumerable<IShape> Read(string filePath);
    }
}
