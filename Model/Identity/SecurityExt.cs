using System.Security.Principal;

namespace opcode4.core.Model.Identity
{
    public static class SecurityExt
    {
        public static T Cast<T>(this IIdentity identity) where T : CustomIdentity
        {
            var vayoIdentity = identity as T;
            return (vayoIdentity ?? new CustomIdentity(identity.Name)) as T;
        }
    }
}
