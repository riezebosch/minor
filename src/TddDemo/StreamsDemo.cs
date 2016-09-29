﻿using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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

        [Fact]
        public void DemoVanDeMemoryStream()
        {
            string input = "readable text";
            using (var stream = new MemoryStream())
            {
                var sw = new StreamWriter(stream);
                sw.Write(input);
                sw.Flush();

                stream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(stream))
                {
                    var result = sr.ReadToEnd();
                    Assert.Equal(input, result);
                }
            }
        }

        [Fact]
        public void AfterEncryptionOriginalTextShouldNotBeVisible()
        {
            string path = "encrypted.data";
            string input = "readable text";
            File.Delete(path);

            using (Aes aes = CreateEncryptor("password"))
            {
                using (var stream = File.Create(path))
                {
                    WriteEncrypted(input, stream, aes);
                }

                using (var stream = File.OpenRead(path))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        var result = sr.ReadToEnd();
                        result.ShouldNotBe(input);
                    }
                }
            }
        }

        [Fact]
        public void EncryptedStreamShouldBeReadableAfterDecrypted()
        {
            string path = "encrypted.data";
            string input = "readable text";
            File.Delete(path);

            using (Aes aes = CreateEncryptor("password"))
            {
                using (var stream = File.Create(path))
                {
                    WriteEncrypted(input, stream, aes);
                }

                using (var stream = File.OpenRead(path))
                {
                    var result = ReadEncrypted(input, aes, stream);
                    result.ShouldBe(input);
                }
            }
        }

        private static string ReadEncrypted(string input, Aes aes, FileStream stream)
        {
            using (var read = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (var sr = new StreamReader(read))
            {
                return sr.ReadToEnd();
            }
        }

        private static void WriteEncrypted(string input, Stream stream, Aes aes)
        {
            using (var write = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var sw = new StreamWriter(write))
            {
                sw.Write(input);
            }
        }

        private static Aes CreateEncryptor(string password)
        {
            var bytes = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes("salt is used to mask the password"));

            var aes = Aes.Create();
            aes.Key = bytes.GetBytes(aes.KeySize / 8);
            aes.IV = bytes.GetBytes(aes.BlockSize / 8);
            return aes;
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
