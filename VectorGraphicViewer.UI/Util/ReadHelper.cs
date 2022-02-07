using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Color = System.Drawing.Color;

namespace VectorGraphicViewer.Util
{
    internal static class ReadHelper
    {
        internal static Point GetPoint(string coordinates)
        {
            var list = coordinates.Replace(",", ".").Split(';');
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");

            var x = Convert.ToDouble(list[0], defaultCulture);
            var y = Convert.ToDouble(list[1], defaultCulture);

            return new Point(x, y);
        }

        internal static Color GetColor(string colorString)
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
