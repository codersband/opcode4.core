using System;
using System.Runtime.Serialization;

namespace opcode4.core.Model
{
    [Serializable, DataContract]
    public abstract class IdentEntityBase
    {
        [IgnoreDataMember]
        public abstract bool HasID { get; }
    }

    [Serializable, DataContract]
    public class IdentEntity<T> : IdentEntityBase
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public virtual T ID { get; set; }

        [IgnoreDataMember]
        public override bool HasID
        {
            get
            {
                if (typeof(T) == typeof(string))
                    return !string.IsNullOrEmpty((string)(object)ID);
                if (typeof(T) == typeof(ulong))
                    return (ulong)(object)ID > 0;
                if (typeof (T) == typeof (ulong?))
                {
                    var id = (ulong?)(object)ID;
                    return id != null && id > 0;
                }
                if (typeof(T) == typeof(long))
                    return (long)(object)ID > 0;
                if (typeof(T) == typeof(int))
                    return (int)(object)ID > 0;
                
                throw new Exception("Unexpected Identity type: " + typeof(T));
            }
        }
    }

    [Serializable, DataContract]
    public class IdentULongEntity : IdentEntity<ulong>
    {
        [IgnoreDataMember]
        public override bool HasID => ID > 0;
    }

    [Serializable, DataContract]
    public class IdentNULongEntity : IdentEntity<ulong?>
    {
        public virtual ulong LID => ID ?? 0;

        [IgnoreDataMember]
        public override bool HasID => ID != null && ID > 0;
    }

    [Serializable, DataContract]
    public class IdentNLongEntity : IdentEntity<long?>
    {
        public virtual long LID => ID ?? 0;

        [IgnoreDataMember]
        public override bool HasID => ID != null && ID > 0;
    }

    [Serializable, DataContract]
    public class IdentStringEntity : IdentEntity<string>
    {
        [IgnoreDataMember]
        public override bool HasID => string.IsNullOrEmpty(ID);
    }
    
}
