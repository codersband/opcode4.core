using System;
using System.Runtime.Serialization;

namespace opcode4.core.Model.Log
{
    [Serializable, DataContract]
    public class LogEntity : TEntity
    {
        [DataMember]
        public virtual LogEventType EventType { get; set; }
        [DataMember]
        public virtual DateTime EventDate { get; set; }
        [DataMember]
        public virtual long? ActorId { get; set; }
        [DataMember]
        public virtual string ActorName { get; set; }
        [DataMember]
        public virtual string FormalMessage { get; set; }
        [DataMember]
        public virtual string Message { get; set; }
        [DataMember]
        public virtual bool HasDetails { get; set; }
        [DataMember]
        public virtual string ServerId { get; set; }
        [DataMember]
        public virtual LogTarget LogTarget { set; get; }
    }
}