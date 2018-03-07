using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using FileScriber;
using FileParser;
using System.Globalization;

namespace FileScriberTest
{
    public class FileWriterTest
    {
        public FileWriterTest()
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        }

        [Fact]
        public void WriteElementString()
        {
            string path = "WriteElementString.txt";
            Writer.Clear(path);

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
        public void WriteMultipleElements()
        {
            string path = "WriteMultipleElements.txt";
            Writer.Clear(path);

            bool _bool = false;
            Writer.Write(path, _bool);

            int _int = 1;
            Writer.Write(path, _int);

            double _double = 3.14159265;
            Writer.Write(path, _double);

            string _string = " Vishy  ";
            Writer.Write(path, _string);

            DateTime _dateTime = new DateTime(1999, 12, 31, 23, 59, 59);
            Writer.WriteLine(path, _dateTime);

            Writer.NextLine(path);      // Extra line

            Writer.WriteLine(path, _bool);
            Writer.WriteLine(path, _int);
            Writer.WriteLine(path, _double);
            Writer.WriteLine(path, _string);

            IParsedFile parsedFile = new ParsedFile(path);
            IParsedLine firstLine = parsedFile.NextLine();

            Assert.Equal(_bool, firstLine.NextElement<bool>());
            Assert.Equal(_int, firstLine.NextElement<int>());
            Assert.Equal(_double, firstLine.NextElement<double>());
            Assert.Equal(_string.Trim(), firstLine.NextElement<string>());

            Assert.Equal(_bool, parsedFile.NextLine().NextElement<bool>());
            Assert.Equal(_int, parsedFile.NextLine().NextElement<int>());
            Assert.Equal(_double, parsedFile.NextLine().NextElement<double>());
            Assert.Equal(_string.Trim(), parsedFile.NextLine().NextElement<string>());

            Assert.True(parsedFile.Empty);
        }

        [Fact]
        public void WriteListDouble()
        {
            string path = "WriteListDouble.txt";
            Writer.Clear(path);

            ICollection<double> vectorToWrite = new List<double>() { 0.0, 1.1, 2.22, 3.33 };
            string textInFile = string.Empty;
            string separator = "%";

            Writer.WriteLine<ICollection<double>, double>(path, vectorToWrite, separator);

            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                string original_line;
                while (!string.IsNullOrEmpty(original_line = reader.ReadLine()))
                {
                    textInFile += original_line;
                }
            }

            List<double> vectorInFile = textInFile.Split(separator.First()).Select(str => double.Parse(str.Trim())).ToList();

            Assert.Equal(vectorToWrite, vectorInFile);
        }

        [Fact]
        public void WriteListString()
        {
            string path = "WriteListString.txt";
            Writer.Clear(path);

            ICollection<string> vectorToWrite = new List<string>() { "string1", "string2", "string3", "string4" };
            string textInFile = string.Empty;
            string separator = "$";

            Writer.WriteLine<ICollection<string>, string>(path, vectorToWrite, separator);

            IParsedFile file = new ParsedFile(path, new char[] { separator.First() });
            while (!file.Empty)
            {
                IParsedLine line = file.NextLine();
                while (!line.Empty)
                {
                    textInFile += line.NextElement<string>();
                    if (!line.Empty)
                        textInFile += "@";
                }
            }

            List<string> vectorInFile = textInFile.Split('@').ToList();

            Assert.Equal(vectorToWrite, vectorInFile);
        }
    }
}
