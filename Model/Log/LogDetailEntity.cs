using System;

namespace opcode4.core.Model.Log
{
    [Serializable]
    public class LogDetailEntity: TEntity
    {
        public virtual string Message { get; set; }
    }
}
