using System.Runtime.Serialization;
using opcode4.core.Data;

namespace opcode4.core.Model.Identity
{
    [DataContract]
    public class RoleItem : TEntity
    {
        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public CustomPermissions Permissions { set; get; }

        [DataMember]
        public string Description { set; get; }
    }
}
