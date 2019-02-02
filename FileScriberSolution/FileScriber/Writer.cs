using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Print = System.Diagnostics.Debug;

namespace FileScriber
{
    static public class Writer
    {
        static public void Clear(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Print.WriteLine(e.Message);
            }
        }

        static public void NextLine(string path)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, true);

                using (writer)
                {
                    writer.WriteLine();
                }
            }
            catch (Exception e)
            {
                // Possible exceptions:
                // FileNotFoundException, DirectoryNotFoundException, IOException, ArgumentException

                Print.WriteLine(e.Message);
                Print.WriteLine("(path: {0}", path);
                throw;
            }
        }

        static public void Write<TCollection, TElement>(string path, TCollection contentToWrite, string separator = " ", bool isNewLine = false) where TCollection : ICollection<TElement>
        {
            string stringVector = string.Join(separator, contentToWrite);

            if (separator.Contains(",") || separator.Contains(".")
                && contentToWrite.Count > 0
                && contentToWrite.First() is double)
            {
                Print.WriteLine("Written text might no be parseable due to coincidence among element separators and number decimal separator ");
            }

            ScribeString(path, stringVector, isNewLine);
        }

        static public void WriteLine<TCollection, TElement>(string path, TCollection contentToWrite, string separator = " ") where TCollection : ICollection<TElement>
        {
            Write<TCollection, TElement>(path, contentToWrite, separator, true);
        }

        static public void Write<T>(string path, T element, string separator = " ", bool isNewLine = false)
        {
            string stringSeparator = isNewLine
                ? string.Empty
                : separator;

            ScribeString(path, Convert.ToString(element) + stringSeparator, isNewLine);
        }

        static public void WriteLine<T>(string path, T element, string separator = " ")
        {
            Write<T>(path, element, separator, true);
        }

        static private void ScribeString(string path, string textInFile, bool isWriteLine = false)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, true);

                using (writer)
                {
                    if (isWriteLine)
                        writer.WriteLine(textInFile);
                    else
                        writer.Write(textInFile);
                }
            }
            catch (Exception e)
            {
                // Possible exceptions:
                // FileNotFoundException, DirectoryNotFoundException, IOException, ArgumentException

                Print.WriteLine(e.Message);
                Print.WriteLine("(path: {0}", path);
                throw;
            }
        }
    }
}
