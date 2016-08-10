namespace opcode4.core.Model.Interfaces.Cache
{
    public interface IStringSerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string str);
    }
}