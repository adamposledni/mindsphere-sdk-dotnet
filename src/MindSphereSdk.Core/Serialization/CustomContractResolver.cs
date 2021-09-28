using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MindSphereSdk.Core.Serialization
{
    /// <summary>
    /// Custom JSON serialization contract resolver.
    /// </summary>
    internal class CustomContractResolver : DefaultContractResolver
    {
        private Type _attribute = typeof(MindSphereName);

        public CustomContractResolver()
        {
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            foreach (JsonProperty prop in list)
            {
                PropertyInfo pi = type.GetProperty(prop.UnderlyingName);
                if (pi != null)
                {
                    var at = pi.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == _attribute) as MindSphereName;

                    if (at != null && !string.IsNullOrWhiteSpace(at.Name))
                    {
                        prop.PropertyName = at.Name;
                    }
                }
            }
            return list;
        }
    }
}
