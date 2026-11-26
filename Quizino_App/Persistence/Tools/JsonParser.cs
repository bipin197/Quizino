using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Persistence.Tools
{
    public class JsonParser<T>
    {
        public IEnumerable<T> Parse(IEnumerable<string> jsonContents)
        {
            var foundObjects = new List<T>();
            foreach(var jsonString in jsonContents)
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<JObject[]>(jsonString);
                    var objects = obj as IEnumerable<T>;
                    if(objects != null)
                    {
                        foundObjects.AddRange(objects);
                        continue;
                    }
                }
                catch(Exception e)
                {
                    continue;
                }

            }

            return foundObjects;
        }

        public T Parse(string jsonContent)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(jsonContent);
                if (obj != null)
                {
                    return obj;
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
      
            return default(T);
        }
    }
}
