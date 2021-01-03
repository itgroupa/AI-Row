using System.Text;

namespace Itgroupa.Common
{
    public static class BinarySerialization
    {
        public static byte[] ToByteArray(string str)
        {
            var result = Encoding.ASCII.GetBytes(str);

            return result;
        }

        public static string GetString(byte[] bytes)
        {
            var result = Encoding.ASCII.GetString(bytes);

            return result;
        }
    }
}