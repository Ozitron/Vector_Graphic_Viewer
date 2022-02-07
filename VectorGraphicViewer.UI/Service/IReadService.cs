using System.Collections.Generic;
using System.Threading.Tasks;
using VectorGraphicViewer.Model.Base;

namespace VectorGraphicViewer.Service
{
    internal interface IReadService
    {
        internal Task<List<IShape>> Read(string filePath);
    }
}
