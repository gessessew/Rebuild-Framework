using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebuild.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    [TestClass]
    public class FilePathTests
    {
        [TestMethod]
        public void Combine()
        {
            new FilePath().Combine("C:\\").ToString().AssertEqual("C:\\");
            new FilePath().Combine("C:\\", "Test1").ToString().AssertEqual("C:\\Test1");
            new FilePath().Combine("C:\\", "Test1", "Test2").ToString().AssertEqual("C:\\Test1\\Test2");
            "C:\\".ToFilePath().Combine("Test1").ToString().AssertEqual("C:\\Test1");
            "C:\\".ToFilePath().Combine("Test1", "Test2").ToString().AssertEqual("C:\\Test1\\Test2");
            "C:\\".ToFilePath().Combine("Test1", "Test2", "Test3").ToString().AssertEqual("C:\\Test1\\Test2\\Test3");
        }

        [TestMethod]
        public void Extension()
        {
            new FilePath().Extension().AssertEqual(string.Empty);
            new FilePath().Extension(".exe").ToString().AssertEqual(string.Empty);
            "test.txt".ToFilePath().Extension().AssertEqual(".txt");
            "test.txt".ToFilePath().Extension(".exe").ToString().AssertEqual("test.exe");
        }

        [TestMethod]
        public void FileName()
        {
            new FilePath().FileName().ToString().AssertEqual(string.Empty);
            new FilePath().FileName("test.txt").ToString().AssertEqual("test.txt");
            "C:\\test.txt".ToFilePath().FileName().ToString().AssertEqual("test.txt");
            "C:\\test.txt".ToFilePath().FileName("file.exe").ToString().AssertEqual("C:\\file.exe");
        }

        [TestMethod]
        public void Parent()
        {
            new FilePath().Parent().ToString().AssertEqual(string.Empty);
            new FilePath().Parent("C:\\").ToString().AssertEqual("C:\\");
            "C:\\File1\\test.txt".ToFilePath().Parent().ToString().AssertEqual("C:\\File1");
            "C:\\File1\\test.txt".ToFilePath().Parent("C:\\File2").ToString().AssertEqual("C:\\File2\\test.txt");
        }

        [TestMethod]
        public void Root()
        {
            new FilePath().Root().ToString().AssertEqual(string.Empty);
            "C:\\File1\\test.txt".ToFilePath().Root().ToString().AssertEqual("C:\\");
        }
    }
}
