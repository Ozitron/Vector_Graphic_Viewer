using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using VectorGraphicViewer.Business.Service.Base;
using VectorGraphicViewer.Model;
using VectorGraphicViewer.Model.Base;
using VectorGraphicViewer.Util;

namespace VectorGraphicViewer.Business.Operation
{
    internal class JsonReaderService : IReaderService
    {
        List<IShape> _shapeList = null!;
        
        async Task<IList<IShape>> IReaderService.Read(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var data = reader.ReadToEnd().ToLower();
            var jsonData = JsonConvert.DeserializeObject<dynamic>(data);
            _shapeList = new List<IShape>();

            ReadJsonDataWithParallel(jsonData);

            return await Task.FromResult(_shapeList);
        }

        private void ReadJsonDataWithParallel(IEnumerable<dynamic> jsonData)
        {
            Parallel.ForEach(jsonData, shape =>
            {
                var color = ReadHelper.GetColor((string)shape.color.Value);

                if (shape.type.Value == Enum.GetName(Shape.line))
                {
                    _shapeList.Add(new Line(new Point[] { ReadHelper.GetPoint(shape.a.Value), ReadHelper.GetPoint(shape.b.Value) }, color));
                }
                else if (shape.type.Value == Enum.GetName(Shape.triangle))
                {
                    bool isFilled = shape.filled == true;
                    _shapeList.Add(new Triangle(
                        new Point[] { ReadHelper.GetPoint(shape.a.Value), ReadHelper.GetPoint(shape.b.Value), ReadHelper.GetPoint(shape.c.Value) },
                        color,
                        isFilled));
                }
                else if (shape.type.Value == Enum.GetName(Shape.circle))
                {
                    var isFilled = shape.filled == true;
                    var radius = (double)shape.radius.Value;
                    var center = ReadHelper.GetPoint(shape.center.Value);

                    _shapeList.Add(new Ellipse(center, radius, isFilled, color));
                }
            });
        }
    }
}
