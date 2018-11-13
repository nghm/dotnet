namespace Hypermedia.AspNetCore.ApiExport.Tests
{
    using System;
    using System.IO;
    using AssemblyAnalyzer;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;

    public class AssemblyLoaderTests
    {
        [Theory]
        [AutoMockData]
        void LoadThrowsArgumentException(string path, AssemblyLoader assemblyLoader)
        {
            Assert.Throws<ArgumentException>(() => assemblyLoader.Load(path));
        }

        [Theory]
        [AutoMockData]
        void LoadThrowsFileNotFoundException(string path, AssemblyLoader assemblyLoader)
        {
            Assert.Throws<FileNotFoundException>(() => assemblyLoader.Load($"c:\\{path}"));
        }
    }
}
