using System;
using System.Runtime.Serialization;
using opcode4.core.Data;

namespace opcode4.core.Model
{
    [Serializable]
    [DataContract]
    public class TEntity: IEntity
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ulong? ID { get; set; }
        [IgnoreDataMember]
        public ulong LID => ID ?? 0;
    }
}
