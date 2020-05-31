using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Persistence.Tools
{
    public static class JsonParser<T>
    {
        internal static IEnumerable<T> Parse(IEnumerable<string> jsonContents)
        {
            var foundObjects = new List<T>();
            foreach(var jsonString in jsonContents)
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(jsonString);
                    foundObjects.Add(obj);
                }
                catch(Exception e)
                {
                    continue;
                }

            }

            return foundObjects;
        }
    }
}
