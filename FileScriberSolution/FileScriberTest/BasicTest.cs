
using System;
using Xunit;

using FileScriber;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileScriberTest
{
    public class FileWriterTest
    {
        [Fact]
        public void WriteLineString()
        {
            string path = "WriteLineString.txt";
            string testString = " This is a  1  2 test! ";
            string textInFile = string.Empty;

            Writer.WriteLine(path, testString);

            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                string original_line;
                while (!string.IsNullOrEmpty(original_line = reader.ReadLine()))
                {
                    textInFile += original_line;
                }
            }

            Assert.Equal(testString, textInFile);
        }

        [Fact]
        public void WriteLineDoubleVector()
        {
            string path = "WriteLineDoubleVector.txt";
            ICollection<double> vectorToWrite = new List<double>() { 0.0, 1.1, 2.22, 3.33 };
            string textInFile = string.Empty;
            char separator = '%';

            Writer.WriteLine(path, vectorToWrite, separator);

            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                string original_line;
                while (!string.IsNullOrEmpty(original_line = reader.ReadLine()))
                {
                    textInFile += original_line;
                }
            }

            List<double> vectorInFile = textInFile.Split(separator).Select(str => double.Parse(str.Trim())).ToList();

            Assert.Equal(vectorToWrite, vectorInFile);
        }
    }
}