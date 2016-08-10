using System;
using System.Linq;
using System.Threading;
using opcode4.core.Data.Provider;
using opcode4.core.Model.Identity;
using opcode4.core.Model.Log;

namespace opcode4.core.Helpers
{
    public static class TActorUtils
    {
        public static bool IsProviderable(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IProviderable));
        }

        public static bool UseProviderConstraint(Type type)
        {
            var t = CurrentActor;
            return t != null && !t.IsInRole($"{Roles.Items.ROOT}") && IsProviderable(type);
        }

        public static bool IsInRole(string role)
        {
            var t = CurrentActor;
            return t != null && t.IsInRole(role);
        }

        public static CustomIdentity CurrentActor => Thread.CurrentPrincipal.Identity as CustomIdentity;

        public static ulong ActorProviderId => CurrentActor?.ProviderId ?? 0;

        public static ulong ActorId => CurrentActor?.Id ?? 0;

        public static string ActorName
        {
            get
            {
                var identity = CurrentActor;
                return identity != null ? identity.Name : string.Empty;
            }
        }

        public static bool IsActorIdentity => ActorId > 0;

        public static bool IsAdministrator
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity as CustomIdentity;
                return identity != null && identity.IsInRole($"{Roles.Items.ADMINISTRATOR}");
            }
        }

        public static bool IsSupervisor
        {
            get
            {
                var identity = CurrentActor;
                return identity != null && identity.IsInRole($"{Roles.Items.ROOT}");
            }
        }

        public static bool ShouldLog(LogEventType logLevel)
        {
            var identity = CurrentActor;
            if (identity == null)
                return true;

            return identity.LogLevel <= logLevel;
        }

    }
}
