using System.Drawing;
using System.Windows.Shapes;

namespace VectorGraphicViewer.UI.Model
{
    public interface IShape
    {
        public Color Color { get; }

        Path GetShape(double scale);
    }
}
