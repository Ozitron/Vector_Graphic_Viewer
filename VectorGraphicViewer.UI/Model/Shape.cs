using System;
using System.Drawing;

namespace VectorGraphicViewer.UI.Model
{
    public abstract class Shape
    {
        protected Guid _id;

        public Color Color { get; }

        public string Id => _id.ToString();

        protected Shape(Color color)
        {
            Color = color;
            _id = new Guid();
        }
    }
}
