using System.Collections.Generic;
using System.Threading.Tasks;
using VectorGraphicViewer.Model.Base;

namespace VectorGraphicViewer.Business.Service.Base
{
    internal interface IReaderService
    {
        internal Task<IList<IShape>> Read(string filePath);
    }
}
