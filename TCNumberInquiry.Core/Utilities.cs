using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace TCNumberInquiry.Core
{

    class Utilities
    {
        private static readonly JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve};
        public static string SafeSerialize(object item)
        {
            if (null == item) return null;
            return JsonSerializer.Serialize(item);
        }

        public static string SafeSerialize(object item, JsonSerializerOptions settings)
        {
            if (null == item) return null;
            return JsonSerializer.Serialize(item, settings);
        }

        public static T SafeDeserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            return JsonSerializer.Deserialize<T>(json, Utilities.defaultSerializerSettings);
        }

        public static T SafeDeserialize<T>(string json, JsonSerializerOptions settings)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            return JsonSerializer.Deserialize<T>(json, settings);
        }

        public static T SafeDeserialize<T>(byte[] buffer)
        {
            var json = Encoding.UTF8.GetString(buffer);
            return Utilities.SafeDeserialize<T>(json);
        }


    }
}
