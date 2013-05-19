using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rebuild.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Assembly assembly, bool inherit = false) where TAttribute : Attribute
        {
            return assembly.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        public static TAttribute GetFirstCustomAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            return assembly.GetCustomAttributes<TAttribute>().FirstOrDefault();
        }
    }
}
