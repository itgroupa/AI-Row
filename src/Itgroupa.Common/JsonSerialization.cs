using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Itgroupa.Common
{
    public static class JsonSerialization
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver() 
        };

        public static T GetObject<T>(string str) where T : class
        {
            var result = JsonConvert.DeserializeObject<T>(str, Settings);

            return result;
        }

        public static string ToString<T>(T obj)
        {
            var result = JsonConvert.SerializeObject(obj, Settings);

            return result;
        }
    }
}