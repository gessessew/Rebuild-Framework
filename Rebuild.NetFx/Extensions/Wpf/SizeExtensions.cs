using System.Windows;

namespace Rebuild.Extensions.Wpf
{
    public static class SizeExtensions
    {
        public static Size Clamp(this Size s, Size minSize, Size maxSize)
        {
            return new Size(
                s.Width.Clamp(minSize.Width, maxSize.Width),
                s.Height.Clamp(minSize.Height, maxSize.Height));
        }

        public static bool IsAlmostEquals(this Size s1, Size s2, double epsilon = double.Epsilon)
        {
            return s1.Width.IsAlmostEqualsTo(s2.Width) 
                && s1.Height.IsAlmostEqualsTo(s2.Height);
        }
    }
}
