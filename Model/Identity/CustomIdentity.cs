using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace opcode4.core.Model.Identity
{
    [DataContract]
    public class CustomIdentity : IIdentity
    {
        [DataMember] 
        private readonly ulong _actorID;

        [DataMember]
        private readonly string _actorName = String.Empty;

        [DataMember]
        private readonly ulong _providerID;

        [DataMember]
        private readonly ArrayList _rolesList = new ArrayList();

        bool IIdentity.IsAuthenticated { get {return (_rolesList.Count > 0);} }

        string IIdentity.AuthenticationType { get { return "aVayosoft"; } }

        public ulong ID { get { return _actorID; } }
        public string Name { get { return _actorName; } }
        public ulong ProviderID { get { return _providerID; } }

        internal bool IsInRole(string role) { return _rolesList.Contains(role); }

        public CustomIdentity()
        { }

        public CustomIdentity(string actorName)
        {
            this._actorName = actorName;
        }

        public CustomIdentity(ulong id, string actorName, string[] roles)
        {
            this._actorID = id;
            this._actorName = actorName;
            this._rolesList.Clear();

            this._rolesList.AddRange(roles);
        }

        public CustomIdentity(ulong id, string actorName, ulong providerId, string[] roles)
        {
            this._actorID = id;
            this._actorName = actorName;
            this._providerID = providerId;
            this._rolesList.Clear();

            this._rolesList.AddRange(roles);
        }
    }
}
