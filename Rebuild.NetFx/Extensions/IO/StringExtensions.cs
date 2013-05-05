using System.IO;

namespace Rebuild.Extensions.IO
{
    public static class StringExtensions
    {
        public static string AppendPath(this string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public static string ChangeExtension(this string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        public static string ChangeFileName(this string path, string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(path), fileName);
        }

        public static string ChangeFileExtension(this string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        public static string GetDirectoryName(this string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static string GetExtension(this string path)
        {
            return Path.GetExtension(path);
        }

        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }

        public static string GetFileNameWithoutExtension(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}
