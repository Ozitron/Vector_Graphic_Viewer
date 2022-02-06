using System.Collections.Generic;
using System.Threading.Tasks;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Service
{
    internal interface IReadService
    {
        internal Task<List<IShape>> Read(string filePath);
    }
}
