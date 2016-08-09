using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using opcode4.core.Data;

namespace opcode4.core.Model.Identity
{
    public class CustomActor : TEntity
    {
        private readonly CustomRoles _roles = new CustomRoles();

        private string _name;
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name");

                _name = value.ToUpper();
            }
        }

        [DataMember]
        public CustomRoles Roles { get { return _roles; } }

        public CustomActor()
        {
            
        }

        public CustomActor(string name, IEnumerable<RoleItem> roles)
        {
            Name = name;

            if(roles != null)
                _roles.AddRange(roles);
        }

        #region rolefunctions

        public bool IsInRole(ulong roleId)
        {
            return Roles.Any(role => role.ID == roleId);
        }

        public bool IsInRole<T>(T roleId) where T : struct , IComparable, IFormattable, IConvertible
        {
            return Roles.Any(role => Enum.ToObject(typeof(T), role.LID).Equals(roleId));
        }

        public bool IsInRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name is not defined");

            if (Roles.Count == 0) return false;

            var rn = roleName.ToLower();
            return Roles.Any(role => role.Name.ToLower().Equals(rn));
        }

        public RoleItem Find(ulong roleId)
        {
            return Roles.FirstOrDefault(r => r.ID == roleId);
        }

        public RoleItem Find(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name is null or empty");

            var rName = roleName.ToLower();
            return Roles.FirstOrDefault(r => r.Name.ToLower().Equals(rName));
        }

        public RoleItem MaxRole()
        {
            if (Roles.Count == 0)
                return null;

            var res = Roles[0];
            foreach (RoleItem r in Roles)
            {
                if (r.ID < res.ID)
                    res = r;
            }

            return res;
        }

        #endregion

    }
}
