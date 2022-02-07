using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VectorGraphicViewer.Model;
using VectorGraphicViewer.Model.Base;
using VectorGraphicViewer.Util;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.Service
{
    public class ReadService : IReadService
    {
        private List<IShape> _shapeList = null!;

        async Task<List<IShape>> IReadService.Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return _shapeList;

            if (Path.GetExtension(filePath) == "." + Enum.GetName(FileType.json))
            {
                return await ReadJsonData(filePath);
            }

            throw new NotImplementedException();
        }

        private void ReadJsonDataWithParallel(IEnumerable<dynamic> jsonData)
        {
            Parallel.ForEach(jsonData, shape =>
            {
                var color = GetColor((string)shape.color.Value);

                if (shape.type.Value == Enum.GetName(Shape.line))
                {
                    _shapeList.Add(new Line(new Point[] { GetPoint(shape.a.Value), GetPoint(shape.b.Value) }, color));
                }
                else if (shape.type.Value == Enum.GetName(Shape.triangle))
                {
                    bool isFilled = shape.filled == true;
                    _shapeList.Add(new Triangle(
                        new Point[] { GetPoint(shape.a.Value), GetPoint(shape.b.Value), GetPoint(shape.c.Value) },
                        color,
                        isFilled));
                }
                else if (shape.type.Value == Enum.GetName(Shape.circle))
                {
                    var isFilled = shape.filled == true;
                    var radius = (double)shape.radius.Value;
                    var center = GetPoint(shape.center.Value);

                    _shapeList.Add(new Ellipse(center, radius, isFilled, color));
                }
            });
        }

        private async Task<List<IShape>> ReadJsonData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var data = reader.ReadToEnd().ToLower();
            var jsonData = JsonConvert.DeserializeObject<dynamic>(data);
            _shapeList = new List<IShape>();

            ReadJsonDataWithParallel(jsonData);

            return await Task.FromResult(_shapeList);
        }

        private static Point GetPoint(string coordinates)
        {
            var list = coordinates.Replace(",", ".").Split(';');
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");

            var x = Convert.ToDouble(list[0], defaultCulture);
            var y = Convert.ToDouble(list[1], defaultCulture);

            return new Point(x, y);
        }

        private static Color GetColor(string colorString)
        {
            var colorArgb = new int[4];
            var i = 0;

            foreach (var s in colorString.Split(';'))
            {
                if (int.TryParse(s.Trim(), out var tempNumber) && i < 4)
                {
                    colorArgb[i] = tempNumber;
                    i += 1;
                }
            }

            return Color.FromArgb(colorArgb[0], colorArgb[1], colorArgb[2], colorArgb[3]);
        }
    }
}
