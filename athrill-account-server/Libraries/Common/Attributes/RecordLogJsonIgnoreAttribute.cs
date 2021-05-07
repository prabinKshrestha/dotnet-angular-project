using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AT.Common.Attributes
{
    public class RecordLogIgnoreAttribute : Attribute
    {
    }
    public class RecordLogContractResolver : DefaultContractResolver
    {
        public new static readonly RecordLogContractResolver Instance = new RecordLogContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member.GetCustomAttributes().Any() && member.GetCustomAttribute<RecordLogIgnoreAttribute>() != null)
            {
                property.ShouldSerialize = Instance => false;
            }

            return property;
        }
    }
}
