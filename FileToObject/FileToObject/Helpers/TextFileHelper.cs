using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileToObject.Helpers
{
    public static class TextFileHelper<T> where T : class, new()
    {

        public static IEnumerable<T> ReadItemsFromFile(string filePath)
        {
            var result = new List<T>();

            bool first = false;
            bool end = false;
            var lines = FileHelper.ReadFileWithItems(filePath);

            foreach (var line in lines)
            {
                end = lines.Last().Equals(line);
                first = lines.First().Equals(line);

                if (string.IsNullOrEmpty(line.Trim()))
                    break;

                if (!first && !end)
                {
                    var dados = line.Split(';');

                    T newItem = new T();
                    result.Add(CreateNewItemFromRow(newItem, dados));

                  //  Console.WriteLine(line);
                }

            }

            return result;
        }

        public static IEnumerable<T> ReadHeaderFromFile(string filePath)
        {
            var result = new List<T>();

            var lines = FileHelper.ReadFileWithItems(filePath);

            foreach (var line in lines)
            {
                var dados = line.Split(';');

                T newItem = new T();
                result.Add(CreateNewItemFromRow(newItem, dados));

                break;
            }

            return result;
        }

        public static IEnumerable<T> ReadFooterFromFile(string filePath)
        {
            var result = new List<T>();

            bool end = false;
            var lines = FileHelper.ReadFileWithItems(filePath);

            foreach (var line in lines)
            {
                end = lines.Last() == line;

                if (string.IsNullOrEmpty(line.Trim()))
                    break;

                if (end)
                {
                    var dados = line.Split(';');

                    T newItem = new T();
                    result.Add(CreateNewItemFromRow(newItem, dados));

                }
            }


            return result;
        }

        public static void WriteFileFromItems(IEnumerable<T> items, string filePath)
        {
            FileHelper.CreateFileIfNotExists(filePath);

            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true);

            foreach (var item in items)
            {
                var line = CreateNewLineFromItem(item);
                file.WriteLine(line);
            }

            file.Close();
        }


        private static string CreateNewLineFromItem(T obj)
        {
            string line = string.Empty;
            var data = new List<string>();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var valueProp = property.GetValue(obj, new object[] { });
                data.Add(valueProp.ToString());
            }
            line = String.Join(";", data.ToArray());
            return line;
        }


        private static T CreateNewItemFromRow(T obj, string[] data)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                property.SetValue(obj, GetDataIndexFromProperty(data, properties, property), null);
            }

            return obj;

        }

        private static string GetDataIndexFromProperty(string[] data, PropertyInfo[] properties, PropertyInfo property)
        {
            var index = GetIndexFromProperty(properties, property);

            return data[index];
        }

        private static int GetIndexFromProperty(PropertyInfo[] properties, PropertyInfo property)
        {
            var index = properties.ToList().FindIndex(x => x.Name == property.Name);
            return index;
        }
    }
}
