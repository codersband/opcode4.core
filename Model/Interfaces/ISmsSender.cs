namespace opcode4.core.Model.Interfaces
{
    public interface ISmsSender
    {
        OperationStatus SendSMS(string phone, string sender, string message);
        OperationStatus SendWakeUpSMS(string phone, string sender, string message);
        string ErrorDesc { get; }
        bool EmulatorMode { get; }
    }
}
