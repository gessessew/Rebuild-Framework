using System;

namespace Rebuild.Extensions
{
    public static class Int16Extensions
    {
        public static short Clamp(this short value, short minValue, short maxValue)
        {
            return Math.Min(Math.Max(value, minValue), maxValue);
        }

        public static short ClamMax(this short value, short maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static short ClampMin(this short value, short minValue)
        {
            return Math.Max(value, minValue);
        }
    }
}
