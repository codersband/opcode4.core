using opcode4.core.Data;

namespace opcode4.core.Exceptions
{
    public class EntityNotFoundException<T>: CustomException where T: TEntity
    {
        public EntityNotFoundException(string tag)
            : base(ExceptionCode.NOT_FOUND, "ENTITY TYPE[" + typeof(T).Name + "] NOT FOUND", tag) { }    

        public EntityNotFoundException(ulong id)
            : base(ExceptionCode.NOT_FOUND, "ENTITY TYPE[" + typeof(T).Name + "] ID[" + id + "] NOT FOUND", "ENTITY_NOT_FOUND") { }
    }
}
