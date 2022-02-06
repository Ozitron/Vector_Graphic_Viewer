using System.Drawing;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model.Base
{
    internal interface ILine : IShape
    {
        Point[] Points { get; }
    }
}
