namespace opcode4.core.Model.Log
{
    public enum LogEventType
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        CriticalError = 4
    }

    public enum LogTarget
    {
        Core = 0,
        Db,
        Memcached,
        Storage,

        OperatorAPI,
        OperatorSMS,

        MobileGeneral,
        MobileNotIdent,

        WebGeneral,
        WebLogon,

        BackEndSvcAvail
    }
}
