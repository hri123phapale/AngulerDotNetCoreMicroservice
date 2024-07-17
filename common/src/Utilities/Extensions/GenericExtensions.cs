using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Helper extension method to check againt null value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static bool IsDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }

        /// <summary>
        /// Helper extension for Linq FirstOrDefault, this will return an alternate object if
        /// default is found to be returned
        /// </summary>
        /// <returns>Alternate object as the default</returns>
        public static T IfDefaultReturn<T>(this T value, T alternate)
        {
            bool areEqual = EqualityComparer<T>.Default.Equals(value, default(T));

            if (areEqual)
            {
                return alternate;
            }

            return value;
        }

        /// <summary>
        /// Extension method to cast object to generic type
        /// </summary>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        /// <summary>
        /// Uses a simple json serialization & deserialization for cloning object
        /// </summary>
        public static T Clone<T>(this T item)
        {
            string serialized = JsonConvert.SerializeObject(item);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        
        /// <summary>
        /// Serializes an object and returns its JSON representation with only the properties
        /// defined in the specified interface type.
        /// </summary>
        /// <typeparam name="T">The interface type that defines the properties to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representation of the interface properties.</returns>
        public static string SerializeInterfaceObject<T> (T obj) where T: class {
            // Use reflection to extract the properties defined in the interface
            var interfaceProperties = typeof (T).GetProperties()
                .Select(p => new {
                    Name = p.Name, Value = p.GetValue(obj)
                })
                .ToList();

            // Create a new object that only contains the properties defined in the interface
            var interfaceObject = new ExpandoObject();
            foreach(var property in interfaceProperties) {
                ((IDictionary<string, object>) interfaceObject).Add(property.Name, property.Value);
            }

            // Serialize the interface object using JsonConvert
            var json = JsonConvert.SerializeObject(interfaceObject);
            return json;
        }
        
        /// <summary>
        /// Creates a new object that implements the specified interface type and contains only the
        /// properties defined in the interface. The property values are copied from the specified
        /// object to the new object.
        /// </summary>
        /// <typeparam name="T">The interface type to implement.</typeparam>
        /// <param name="obj">The object to extract properties from.</param>
        /// <returns>A new object that implements the specified interface type and contains only the
        /// properties defined in the interface.</returns>
        public static T CreateInterfaceObject<T>(object obj) where T: class {
            // Use reflection to extract the properties defined in the interface
            var interfaceProperties = typeof(T).GetProperties()
                .Select(p => new {
                    Name = p.Name, Value = p.GetValue(obj)
                })
                .ToList();

            // Create a new object that only contains the properties defined in the interface
            var interfaceObject = Activator.CreateInstance<T>();
            foreach(var property in interfaceProperties) {
                var interfaceProperty = typeof(T).GetProperty(property.Name);
                if (interfaceProperty != null) {
                    interfaceProperty.SetValue(interfaceObject, property.Value);
                }
            }

            // Return the interface object
            return interfaceObject;
        }
    }
}