using System;
using System.Drawing;
using System.Globalization;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.UI.Helper
{
    internal static class ReadHelper
    {
        internal static Point GetPoint(string coordinates)
        {
            string[] list = coordinates.Replace(",", ".").Split(';');
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");

            double x = Convert.ToDouble(list[0], defaultCulture);
            double y = Convert.ToDouble(list[1], defaultCulture);

            return new Point(x, y);
        }

        internal static Color GetColor(string colorString)
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

    }
}
