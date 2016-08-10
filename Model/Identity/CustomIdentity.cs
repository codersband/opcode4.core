using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Principal;
using opcode4.core.Model.Log;

namespace opcode4.core.Model.Identity
{
    [DataContract]
    public class CustomIdentity : IIdentity
    {
        [DataMember] 
        private readonly ulong _actorId;

        [DataMember]
        private readonly string _actorName = string.Empty;

        [DataMember]
        private readonly ulong _providerId;

        [DataMember]
        private readonly ArrayList _rolesList = new ArrayList();

        [DataMember]
        public LogEventType LogLevel { set; get; }

        bool IIdentity.IsAuthenticated => (_rolesList.Count > 0);

        string IIdentity.AuthenticationType => "custom";

        public ulong Id => _actorId;
        public string Name => _actorName;
        public ulong ProviderId => _providerId;

        internal bool IsInRole(string role) { return _rolesList.Contains(role); }

        public CustomIdentity()
        { }

        public CustomIdentity(string actorName)
        {
            this._actorName = actorName;
        }

        public CustomIdentity(ulong id, string actorName, string[] roles)
        {
            this._actorId = id;
            this._actorName = actorName;
            this._rolesList.Clear();

            this._rolesList.AddRange(roles);
        }

        public CustomIdentity(ulong id, string actorName, ulong providerId, string[] roles)
        {
            this._actorId = id;
            this._actorName = actorName;
            this._providerId = providerId;
            this._rolesList.Clear();

            this._rolesList.AddRange(roles);
        }
    }
}
