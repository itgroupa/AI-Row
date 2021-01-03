using System.IO;

namespace Itgroupa.Common
{
    public static class FileHelper
    {
        public static void SaveToFile<T>(T obj, string fileName) where T : class
        {
            var str = JsonSerialization.ToString(obj);
            var bytes = BinarySerialization.ToByteArray(str);
            
            File.WriteAllBytes(fileName, bytes);
        }

        public static T GetFromFile<T>(string fileName) where T : class
        {
            var bytes =  File.ReadAllBytes(fileName);
            var str = BinarySerialization.GetString(bytes);
            var obj = JsonSerialization.GetObject<T>(str);

            return obj;
        }
    }
}