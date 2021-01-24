using System;
using System.Collections.Generic;
using System.Text;

namespace FileToObject.Helpers
{
    public static class FileHelper
    {
        public static void CreateFileIfNotExists(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                var file = System.IO.File.CreateText(filePath);
                file.Close();
            }
        }

        public static void DeleteFileWithItems(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        public static List<string> ReadFileWithItems(string filePath)
        {
            var result = new List<string>();

            int counter = 0;
            string line;

            if (!System.IO.File.Exists(filePath))
                return result;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
                new System.IO.StreamReader(filePath);

            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                    break;

                result.Add(line);

                Console.WriteLine(string.Format("{0} - {1}", counter, line));
                counter++;
            }

            file.Close();

            return result;
        }
    }
}
