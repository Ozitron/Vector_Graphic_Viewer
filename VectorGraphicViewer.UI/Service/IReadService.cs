using System.Collections.Generic;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Service
{
    internal interface IReadService
    {
        internal List<IShape> Read(string filePath);
    }
}
