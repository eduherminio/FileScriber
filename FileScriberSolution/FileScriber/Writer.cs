using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


//using Print = System.Console;
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
                throw e;
            }
        }

        static public void Write<T>(string path, ICollection<T> contentToWrite, char separator = ' ', bool isNewLine = false)
        {
            string stringVector = String.Join(separator.ToString(), contentToWrite);

            if ((separator == ',' || separator == '.')
                && contentToWrite.Count > 0
                && contentToWrite.First().GetType() == typeof(double))
            {
                Print.WriteLine("Written text might no be parseable due to coincidence among element separators and number decimal separator ");
            }

            ScribeString(path, stringVector, isNewLine);
        }

        static public void WriteLine<T>(string path, ICollection<T> contentToWrite, char separator = ' ')
        {
            Write(path, contentToWrite, separator, true);
        }

        static public void Write<T>(string path, T element, char separator = ' ', bool isNewLine = false)
        {
            string stringSeparator = isNewLine == true
                ? string.Empty
                : separator.ToString();

            ScribeString(path, Convert.ToString(element) + stringSeparator, isNewLine);
        }

        static public void WriteLine<T>(string path, T element, char separator = ' ')
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
                throw e;
            }
        }
    }
}
