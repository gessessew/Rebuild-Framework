using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rebuild.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetCompany(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyCompanyAttribute>().IfNotNull(a => a.Company);
        }

        public static string GetCopyright(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().IfNotNull(a => a.Copyright);
        }

        public static string GetCulture(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyCultureAttribute>().IfNotNull(a => a.Culture);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            return assembly.GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Assembly assembly, bool inherit = false) where TAttribute : Attribute
        {
            return assembly.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        public static string GetDefaultAlias(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyDefaultAliasAttribute>().IfNotNull(a => a.DefaultAlias);
        }

        public static string GetDescription(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().IfNotNull(a => a.Description);
        }

        public static string GetFileVersion(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().IfNotNull(a => a.Version);
        }

        public static string GetProduct(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyProductAttribute>().IfNotNull(a => a.Product);
        }

        public static string GetTitle(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyTitleAttribute>().IfNotNull(a => a.Title);
        }

        public static string GetTrademark(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyTrademarkAttribute>().IfNotNull(a => a.Trademark);
        }

        public static string GetVersion(this Assembly assembly)
        {
            return assembly.GetCustomAttribute<AssemblyVersionAttribute>().IfNotNull(a => a.Version);
        }
    }
}
