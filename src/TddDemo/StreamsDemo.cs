﻿using System;
using System.Collections.Generic;
using System.IO;
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
            var inputPath = "temp.txt";
            var outputPath = "output.zip";

            File.Delete(inputPath);
            File.WriteAllText(inputPath, "some dummy content");

            using (var input = File.OpenRead(inputPath))
            using (var output = File.Create(outputPath))
            {
                Copy(input, output);
            }

            Assert.True(File.Exists(inputPath));
            Assert.True(File.Exists(outputPath));
            Assert.Equal(File.ReadAllText(inputPath), File.ReadAllText(outputPath));
        }

        private static void Copy(Stream input, Stream output)
        {
            input.CopyTo(output);
        }
    }
}
