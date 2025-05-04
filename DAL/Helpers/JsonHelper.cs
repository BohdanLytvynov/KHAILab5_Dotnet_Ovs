using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace DAL.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return JsonConvert.SerializeObject(value);        
        }

        public static T Deserialize<T>(string value, T obj)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            
            return JsonConvert.DeserializeAnonymousType<T>(value, obj);
        }
    }
}
