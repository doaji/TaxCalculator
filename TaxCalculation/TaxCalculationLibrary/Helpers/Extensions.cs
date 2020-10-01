using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculationLibrary.Helpers
{
    public static class Extensions
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotEmpty(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static string Jsonify(this object item)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(item);
        }

        public static T UnJsonify<T>(this string item, Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (jsonSerializerSettings != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(item, jsonSerializerSettings);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(item);
        }
    }
}