using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using opcode4.core.Model;

namespace opcode4.core.Data.Provider
{
    [DataContract]
    public class ProviderEntity : IdentNULongEntity
    {
        [DataMember, JsonProperty("name")]
        public virtual string Name { get; set; }
        [DataMember, JsonProperty("descr")]
        public virtual string Description { get; set; }
        [DataMember, JsonProperty("culture")]
        public virtual CultureInfo Culture { set; get; }
    }
}
