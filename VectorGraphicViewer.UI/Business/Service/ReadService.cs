﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using VectorGraphicViewer.UI.Helper;
using VectorGraphicViewer.UI.Model;
using VectorGraphicViewer.UI.Model.Base;

namespace VectorGraphicViewer.UI.Business.Service
{
    public class ReadService : IReadService
    {
        private readonly List<IShape> _shapeList = new();

        IEnumerable<IShape> IReadService.Read(string filePath)
        {
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
                var color = ReadHelper.GetColor(shape.FirstOrDefault(x => x.Key == "color").Value);

                if (shape.First(x => x.Key == "type").Value == "line")
                {
                    _shapeList.Add(new Line
                    (
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "a").Value),
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "b").Value),
                        color
                    ));
                }
                else if (shape.FirstOrDefault(x => x.Key == "type").Value == "triangle")
                {
                    _shapeList.Add(new Triangle
                    (
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "a").Value),
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "b").Value),
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "c").Value),
                        shape.FirstOrDefault(x => x.Key == "isFilled").Value == "true",
                        color
                    ));
                }
                else if (shape.FirstOrDefault(x => x.Key == "type").Value == "circle")
                {
                    _shapeList.Add(new Ellipse
                    (
                        ReadHelper.GetPoint(shape.FirstOrDefault(x => x.Key == "center").Value),
                        Convert.ToDouble((shape.FirstOrDefault(x => x.Key == "radius").Value)),
                        shape.FirstOrDefault(x => x.Key == "isFilled").Value == "true",
                        color
                    ));
                }
            }

            return _shapeList;
        }
    }
}