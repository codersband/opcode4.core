using System;
using System.Linq;
using System.Threading;
using opcode4.core.Data.Provider;
using opcode4.core.Model.Identity;
using opcode4.core.Model.Log;

namespace opcode4.core.Helpers
{
    public static class IdentityUtils
    {
        public static bool IsProviderable(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IProviderable));
        }

        public static bool UseProviderConstraint(Type type)
        {
            var t = CurrentIdentity;
            return t != null && !t.IsInRole($"{IdentityRoles.Items.ROOT}") && IsProviderable(type);
        }

        public static bool IsInRole(string role)
        {
            var t = CurrentIdentity;
            return t != null && t.IsInRole(role);
        }

        public static CustomIdentity CurrentIdentity => Thread.CurrentPrincipal.Identity as CustomIdentity;

        public static long ProviderId => CurrentIdentity?.ProviderId ?? 0;

        public static long Id => CurrentIdentity?.Id ?? 0;

        public static string ActorName
        {
            get
            {
                var identity = CurrentIdentity;
                return identity != null ? identity.Name : string.Empty;
            }
        }

        public static bool IsIdentityInitialized => Id > 0;

        public static bool IsAdministrator
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity as CustomIdentity;
                return identity != null && identity.IsInRole($"{IdentityRoles.Items.ADMINISTRATOR}");
            }
        }

        public static bool IsSupervisor
        {
            get
            {
                var identity = CurrentIdentity;
                return identity != null && identity.IsInRole($"{IdentityRoles.Items.ROOT}");
            }
        }

        public static bool ShouldLog(LogEventType logLevel)
        {
            var identity = CurrentIdentity;
            if (identity == null)
                return true;

            return identity.LogLevel <= logLevel;
        }

    }
}
