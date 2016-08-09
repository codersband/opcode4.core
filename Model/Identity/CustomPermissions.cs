using System;

namespace opcode4.core.Model.Identity
{
    [Flags]
    public enum CustomPermissions
    {
        Read = 1,
        Write = 2,
        Execute = 4
    }
}
