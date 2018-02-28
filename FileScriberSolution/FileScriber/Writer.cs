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
        static public void WriteLine<T>(string path, ICollection<T> contentToWrite, char separator = ' ')
        {
            string stringVector = String.Join(separator.ToString(), contentToWrite);
            if ((separator == ',' || separator == '.')
                && contentToWrite.Count > 0
                && contentToWrite.First().GetType() == typeof(double))
            {
                Print.WriteLine("Written text might no be parseable due to coincidence among element separators and number decimal separator ");
            }

            WriteLine(path, stringVector);
        }

        static public void WriteLine(string path, string textInFile)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);

                using (writer)
                {
                    writer.WriteLine(textInFile);
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
