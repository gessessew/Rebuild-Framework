
namespace Rebuild.Extensions
{
    public static class Int32Extensions
    {
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            return value < minValue ? minValue : value > maxValue ? maxValue : value;
        }
    }
}
