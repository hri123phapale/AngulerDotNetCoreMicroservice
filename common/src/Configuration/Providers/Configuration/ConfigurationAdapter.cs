using Newtonsoft.Json;

namespace Blogposts.Common.Configuration.Providers.Configuration
{
    /// <summary>
    /// Extension to support for the new payload format for the configuration provider
    /// </summary>
    public static class ConfigurationAdapter
    {
        /// <summary>
        /// Extract content of "payload/data" of json string
        /// </summary>
        /// <param name="inputJson">configuration text as json string</param>
        /// <returns>A <see cref="string"/> containing a configuration</returns>
        public static string Convert(this string inputJson)
        {
            dynamic jsonObj = JsonConvert.DeserializeObject(inputJson);
            var realData = jsonObj.payload.data;
            string configJson = realData.ToString();

            return configJson;
        }
    }
}