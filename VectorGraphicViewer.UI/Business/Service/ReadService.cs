using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using VectorGraphicViewer.UI.Model;
using VectorGraphicViewer.UI.Model.Base;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Business.Service
{
    public class ReadService : IReadService
    {
        private readonly List<IShape> _shapeList = new();

        List<IShape> IReadService.Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return _shapeList;

            _shapeList.Clear();
            using var reader = new StreamReader(filePath);
            var rawData = reader.ReadToEnd().ToLower();
            rawData = rawData.Replace("true", "\"true\""); // TEMP fix
            rawData = rawData.Replace("false", "\"false\""); // TEMP fix
            rawData = rawData.Replace(".", ","); // TEMP fix

            using var doc = JsonDocument.Parse(rawData);
            var root = doc.RootElement;

            var shapes = root.EnumerateArray();

            while (shapes.MoveNext())
            {
                var shape = shapes.Current.Deserialize<Dictionary<string, string>>();
                var color = GetColor(shape.FirstOrDefault(x => x.Key == "color").Value);

                if (shape.First(x => x.Key == "type").Value == "line")
                {
                    _shapeList.Add(new LinearShape
                    (
                        GetPoint(shape.FirstOrDefault(x => x.Key == "a").Value),
                        GetPoint(shape.FirstOrDefault(x => x.Key == "b").Value),
                        color
                    ));
                }
                else if (shape.FirstOrDefault(x => x.Key == "type").Value == "triangle")
                {
                    _shapeList.Add(new Triangle
                    (
                        GetPoint(shape.FirstOrDefault(x => x.Key == "a").Value),
                        GetPoint(shape.FirstOrDefault(x => x.Key == "b").Value),
                        GetPoint(shape.FirstOrDefault(x => x.Key == "c").Value),
                        shape.FirstOrDefault(x => x.Key == "filled").Value == "true",
                        color
                    ));
                }
                else if (shape.FirstOrDefault(x => x.Key == "type").Value == "circle")
                {
                    _shapeList.Add(new Ellipse
                    (
                        GetPoint(shape.FirstOrDefault(x => x.Key == "center").Value),
                        Convert.ToDouble((shape.FirstOrDefault(x => x.Key == "radius").Value)),
                        shape.FirstOrDefault(x => x.Key == "filled").Value == "true",
                        color
                    ));
                }
            }

            return _shapeList;
        }

        private static Point GetPoint(string coordinates)
        {
            string[] list = coordinates.Replace(",", ".").Split(';');
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");

            double x = Convert.ToDouble(list[0], defaultCulture);
            double y = Convert.ToDouble(list[1], defaultCulture);

            return new Point(x, y);
        }

        private static Color GetColor(string colorString)
        {
            int[] colorArgb = new int[4];
            int i = 0;

            foreach (string s in colorString.Split(';'))
            {
                if (int.TryParse(s.Trim(), out var tempNumber) && i < 4)
                {
                    colorArgb[i] = tempNumber;
                    i += 1;
                }
            }

            return Color.FromArgb(colorArgb[0], colorArgb[1], colorArgb[2], colorArgb[3]);
        }


        private static void TryDeserialize(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };


            string fileName = filePath;
            string jsonString = File.ReadAllText(fileName);
            TriangleModel test = JsonSerializer.Deserialize<TriangleModel>(jsonString, options)!;
        }
    }

    public class LineModel
    {
        public string Type { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string Color { get; set; }
    }

    public class TriangleModel
    {
        public string Type { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string Filled { get; set; }
        public string Color { get; set; }
    }

}
