using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VectorGraphicViewer.Business.Operation;
using VectorGraphicViewer.Business.Service;
using VectorGraphicViewer.Model.Base;
using VectorGraphicViewer.Util;

namespace VectorGraphicViewer.Business
{
    internal static class ReadData
    {
        internal static async Task<IList<IShape>> Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            if (Path.GetExtension(filePath) == "." + Enum.GetName(FileType.json))
            {
                var readService = new ReadService(new JsonReaderService());
                return await readService.ReadFile(filePath);
            }

            throw new NotImplementedException();
        }
    }
}
