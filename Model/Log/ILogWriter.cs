namespace opcode4.core.Model.Log
{
    public interface ILogWriter
    {
        string AddEvent(LogEntity entity);
    }
}
