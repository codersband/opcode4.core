using System;

namespace opcode4.core.Data.Criterias
{
    public class Criteria<TCriteria> where TCriteria : ICriteria
    {
        private readonly TCriteria _repository;

        public Criteria(TCriteria repository)
        {
            _repository = repository;
        }

        public TReturn Select<TReturn>(Func<TCriteria, TReturn> code)
        {
            return code(_repository);
        }

        public void RunCommand(Action<TCriteria> code)
        {
            code(_repository);
        }
    
    }
}
