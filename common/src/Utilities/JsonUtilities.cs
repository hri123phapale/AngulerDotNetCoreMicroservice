using Blogposts.Common.Utilities.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Blogposts.Common.Utilities
{
    public static class JsonUtilities
    {
        /// <summary>
        /// Remove fields from a string json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="fields">The fields</param>
        /// <returns>The new json</returns>
        public static string RemoveFields(string json, List<string> fields)
        {
            var jsonObject = (JObject)JsonConvert.DeserializeObject(json);
            foreach (string field in fields)
            {
                string fieldCamelCase = field.ToCamelCase();
                if (jsonObject[fieldCamelCase] != null)
                {
                    jsonObject.Property(fieldCamelCase).Remove();
                }
            }

            return jsonObject.ToString();
        }

        /// <summary>
        /// Creates a stream reader from the path that the file is located, reads, and deserializes the json
        /// </summary>
        /// <typeparam name="T">The type of the entities we are going to read</typeparam>
        /// <param name="filePath">The file path on the server</param>
        /// <returns>An enumerable containing the entities</returns>
        public static IEnumerable<T> ParseJsonFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
    }
}