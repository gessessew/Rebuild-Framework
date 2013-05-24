using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rebuild.IO
{
    public struct FilePath : IEquatable<FilePath>, IEquatable<string>
    {
        private readonly string _path;

        public FilePath(string path)
        {
            _path = path;
        }

        public FilePath Combine(string path)
        {
            return new FilePath(_path == null ? path : Path.Combine(_path, path));
        }

        public FilePath Combine(string path1, string path2)
        {
            return new FilePath(_path == null ? Path.Combine(path1, path2) : Path.Combine(_path, path1, path2));
        }

        public FilePath Combine(string path1, string path2, string path3)
        {
            return new FilePath(_path == null ? Path.Combine(path1, path2, path3) : Path.Combine(_path, path1, path2, path3));
        }

        public string Extension()
        {
            return _path == null ? string.Empty : Path.GetExtension(_path);
        }

        public FilePath Extension(string extension)
        {
            return new FilePath(Path.ChangeExtension(_path, extension));
        }

        public FilePath FileName()
        {
            return new FilePath(_path == null ? null : Path.GetFileName(_path));
        }

        public FilePath FileName(string fileName)
        {
            return new FilePath(_path == null ? fileName : Path.Combine(Path.GetDirectoryName(_path), fileName));
        }

        public FilePath FileNameWithoutExtension()
        {
            return new FilePath(_path == null ? null : Path.GetFileNameWithoutExtension(_path));
        }

        public FilePath FileNameWithoutExtension(string filename)
        {
            return new FilePath(_path == null ? filename : (Path.Combine(Path.GetDirectoryName(_path), filename) + Path.GetExtension(_path)));
        }

        public override bool Equals(object obj)
        {
            if (obj is FilePath)
            {
                return Equals((FilePath)obj);
            }

            var s = obj as string;
            return s != null && Equals(s);
        }

        public bool Equals(FilePath other)
        {
            return Equals(other._path);
        }

        public bool Equals(string other)
        {
            return String.Equals(_path, other);
        }

        public override int GetHashCode()
        {
            return _path == null ? 0 : _path.GetHashCode();
        }

        public FilePath Parent()
        {
            return new FilePath(_path == null ? null : Path.GetDirectoryName(_path));
        }

        public FilePath Parent(string path)
        {
            return new FilePath(_path == null ? path : Path.Combine(path, Path.GetFileName(_path)));
        }

        public FilePath Root()
        {
            return new FilePath(_path == null ? null : Path.GetPathRoot(_path));
        }

        public override string ToString()
        {
            return _path ?? string.Empty;
        }

        public static implicit operator string(FilePath p)
        {
            return p._path;
        }

        public static implicit operator FilePath(string p)
        {
            return new FilePath(p);
        }

        public static FilePath operator +(FilePath fp, string v)
        {
            return new FilePath(Path.Combine(fp._path, v));
        }

        public static FilePath operator +(string v, FilePath fp)
        {
            return new FilePath(Path.Combine(v, fp._path));
        }

        public static FilePath operator +(FilePath x, FilePath y)
        {
            return new FilePath(Path.Combine(x._path, y._path));
        }
    }
}
