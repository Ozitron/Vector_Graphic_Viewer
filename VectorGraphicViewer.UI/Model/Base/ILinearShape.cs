using System.Drawing;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Model.Base;

internal interface ILinearShape : ILine
{
    bool IsFilled { get; }
}