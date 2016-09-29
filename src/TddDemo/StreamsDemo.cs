using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class StreamsDemo
    {
        [Fact]
        public void HoeWerkJeMetFilesEnStreams()
        {
            var stream = File.Open("demo.txt", FileMode.Create);
            var writer = new StreamWriter(stream);

            writer.Write("Hoi");
            writer.Flush();

            writer.WriteLine();
            writer.Write("Dit wordt daarna weggeschreven");

            writer.Dispose();
            stream.Dispose();

            Assert.Equal("Hoi\r\nDit wordt daarna weggeschreven", File.ReadAllText("demo.txt"));
        }

        [Fact]
        public void IsDeDisposeVoldoendeOfMoetenWeHetNogAfbakenenMetEenUsing()
        {
            try
            {
                var stream = File.Open("using.txt", FileMode.Create);
                var writer = new StreamWriter(stream);
                try
                {
                    WriteSomethingToTheStream(writer);
                }
                finally
                {
                    stream.Dispose();
                    writer.Dispose();
                }
            }
            catch
            {
                // Swallow the exception for the test to continue
            }

            var other = File.OpenRead("using.txt");
            other.Dispose();
        }


        [Fact]
        public void DeDisposeInEenFinallyIsVoldoendeMaarDatDoenWeMetEenUsing()
        {
            try
            {
                using (var stream = File.Open("using.txt", FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    WriteSomethingToTheStream(writer);
                }
            }
            catch
            {
                // Swallow the exception for the test to continue
            }

            using (var other = File.OpenRead("using.txt"))
            {
            }
        }

        private void WriteSomethingToTheStream(StreamWriter writer)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void KanIkStreamsInElkaarVouwen()
        {
            // Arrange
            var inputPaths = new List<string> { "temp1.txt", "temp2.txt" };
            var outputPath = "output.txt";

            var data = CreateInputFiles(inputPaths);

            // Act
            CopyFilesToOutput(inputPaths, outputPath);

            // Assert
            inputPaths.ShouldAllBe(file => File.Exists(file));
            Assert.True(File.Exists(outputPath));

            CompareInputsToExtractedOutput(inputPaths, outputPath);
        }

        private static void CompareInputsToExtractedOutput(List<string> inputPaths, string outputPath)
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(temp);

            ZipFile.ExtractToDirectory(outputPath, temp);

            foreach (var inputFile in inputPaths)
            {
                var outputFile = Path.Combine(temp, inputFile);
                Assert.True(File.Exists(outputFile));
                Assert.Equal(File.ReadAllText(inputFile), File.ReadAllText(outputFile));
            }
        }

        private static IEnumerable<Guid> CreateInputFiles(List<string> inputPaths)
        {
            inputPaths.ForEach(File.Delete);
            return inputPaths.Select(file => { var guid = Guid.NewGuid(); File.WriteAllText(file, guid.ToString()); return guid; }).ToList();
        }

        private static void CopyFilesToOutput(IEnumerable<string> inputPaths, string outputPath)
        {
            using (var zip = File.Open(outputPath, FileMode.Create))
            using (var archive = new ZipArchive(zip, ZipArchiveMode.Create))
            {
                foreach (var inputPath in inputPaths)
                {
                    var entry = archive.CreateEntry(inputPath);
                    using (var input = File.OpenRead(inputPath))
                    using (var output = entry.Open())
                    {
                        input.CopyTo(output);
                    }
                }
            }
        }
    }
}
