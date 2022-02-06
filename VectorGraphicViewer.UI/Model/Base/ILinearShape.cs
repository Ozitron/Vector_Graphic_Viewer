using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model.Base
{
    internal interface ILinearShape : IShape
    {
        Point[] Points { get; }
    }
}
