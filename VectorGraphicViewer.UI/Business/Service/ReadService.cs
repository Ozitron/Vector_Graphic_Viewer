using System.Collections.Generic;
using System.Threading.Tasks;
using VectorGraphicViewer.Business.Service.Base;
using VectorGraphicViewer.Model.Base;

namespace VectorGraphicViewer.Business.Service
{
    public class ReadService
    {
        private readonly IReaderService _readerService;

        internal ReadService(IReaderService readerService)
        {
            _readerService = readerService;
        }

        public async Task<IList<IShape>> ReadFile(string filePath)
        {
            return await _readerService.Read(filePath);
        }
    }
}
