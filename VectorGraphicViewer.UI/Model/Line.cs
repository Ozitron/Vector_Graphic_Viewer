﻿using System.Drawing;
using Point = System.Windows.Point;


namespace VectorGraphicViewer.UI.Model
{
    internal class Line : LinearShape
    {
        public override Color Color { get; }
        public sealed override Point[] Points { get; set; }

        public Line(Point a, Point b, Color color)
        {
            Color = Color.Black;
            Points = new[] { a, b };
        }
    }
}
