using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using opcode4.utilities;

namespace opcode4.core.Model.Identity
{
    [DataContract]
    public class CustomRoles : List<RoleItem>
    {
        public ulong ToInt64()
        {
            return this.Aggregate(0UL, (current, actorRole) => current | actorRole.LID);
        }
    }

    [DataContract]
    public abstract class Roles<TEnum> : CustomRoles where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        protected Roles()
        {
            if (typeof(TEnum).GetCustomAttributes(typeof(FlagsAttribute), false).Length == 0)
                throw new InvalidOperationException("Enum must use [Flags].");
        }
    }


    [DataContract]
    public class Roles : Roles<Roles.Items>
    {
        [Flags]
        [DataContract]
        public enum Items
        {
            [EnumMember]
            ROOT = 1,
            [EnumMember]
            ADMINISTRATOR = 2,
            [EnumMember]
            OPERATOR = 4,
            [EnumMember]
            USER = 8,
            [EnumMember]
            HANDSET = 16,
            [EnumMember]
            SERVICE = 32,
            [EnumMember]
            TRACE = 64,
            [EnumMember]
            TALKMAN = 128,

            [EnumMember]
            EXTENTION = 1024
        }

        public static List<RoleItem> Parse(Items roles)
        {
            var result = new List<RoleItem>();
            EnumUtils<Items>.ForEach(key =>
            {
                if (roles.HasFlag(key))
                {
                    var role = new RoleItem
                    {
                        ID = (ulong)key,
                        Name = key.ToString()
                    };

                    switch (key)
                    {
                        case Items.ROOT:
                            role.Permissions = CustomPermissions.Write | CustomPermissions.Read | CustomPermissions.Execute;
                            break;
                        case Items.ADMINISTRATOR:
                            role.Permissions = CustomPermissions.Write | CustomPermissions.Read | CustomPermissions.Execute;
                            break;
                        case Items.OPERATOR:
                            role.Permissions = CustomPermissions.Write | CustomPermissions.Read | CustomPermissions.Execute;
                            break;
                        case Items.USER:
                            role.Permissions = CustomPermissions.Read;
                            break;
                        case Items.HANDSET:
                            role.Permissions = CustomPermissions.Write | CustomPermissions.Read;
                            break;
                        case Items.SERVICE:
                            role.Permissions = CustomPermissions.Write | CustomPermissions.Read;
                            break;
                        case Items.TRACE:
                            role.Permissions = CustomPermissions.Write;
                            break;
                        case Items.EXTENTION: 
                            break;
                    }
                    result.Add(role);
                }
            });

            return result;
        }
    }
}
