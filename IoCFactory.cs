using opcode4.core.Data;
using opcode4.core.Model.Interfaces.Cache;
using opcode4.ioc.lightinject;

namespace opcode4.core
{
    public class IoCFactory
    {
        public static IDAO DAO => IocRegistrator.GetObject<IDAO>();
        public static ICacheProvider MemoryCache => IocRegistrator.GetObject<ICacheProvider>();
    }
}
