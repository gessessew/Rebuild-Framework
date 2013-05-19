using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rebuild.Extensions
{
    public static class MemberInfoExtensions
    {
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this MemberInfo type, bool inherit = false) where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }
    }
}
