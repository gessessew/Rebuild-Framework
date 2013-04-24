
namespace Rebuild.Utils
{
    public static class Doubles
    {
        public static double Lerp(double from, double to, double ratio)
        {
            return (to - from) * ratio + from;
        }
    }
}
