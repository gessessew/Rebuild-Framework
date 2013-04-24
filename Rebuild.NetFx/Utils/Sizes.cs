using System;
using System.Windows;

namespace Rebuild.Utils
{
    public static class Sizes
    {
        public static Size MaxWidthHeight(Size s1, Size s2)
        {
            return new Size(
                Math.Max(s1.Width, s2.Width),
                Math.Max(s1.Height, s2.Height));
        }

        public static Size MinWidthHeight(Size s1, Size s2)
        {
            return new Size(
                Math.Min(s1.Width, s2.Width),
                Math.Min(s1.Height, s2.Height));
        }
    }
}
