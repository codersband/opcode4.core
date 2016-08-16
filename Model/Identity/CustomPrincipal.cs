using System;
using System.Security.Principal;
using System.Threading;

namespace opcode4.core.Model.Identity
{
    [Serializable]
    public class CustomPrincipal : IPrincipal
    {
        readonly CustomIdentity _identityField;

        IIdentity IPrincipal.Identity => _identityField;

        bool IPrincipal.IsInRole(string role) { return _identityField.IsInRole(role); }

        public CustomPrincipal(CustomIdentity identity) 
        {
            AppDomain currentdomain = Thread.GetDomain();
            currentdomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);

            IPrincipal oldPrincipal = Thread.CurrentPrincipal;
            Thread.CurrentPrincipal = this;

            try
            {
                if (!(oldPrincipal.GetType() == typeof(CustomIdentity)))
                    currentdomain.SetThreadPrincipal(this);
            }
            catch{}

            _identityField = identity;
        }
    }
}
