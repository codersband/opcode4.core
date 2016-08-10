using opcode4.core.Data.Criterias;
using opcode4.core.Model;

namespace opcode4.core.Data
{
    public abstract class GDAO<TId> : IGDAO<TId>
    {
        protected abstract IRepository<T, TId> GetRepository<T>() where T : IdentEntity<TId>;
        
        public T Get<T>(TId id) where T : IdentEntity<TId>
        {
            return (GetRepository<T>()).Get(id);
        }

        public void Set<T>(T entity) where T : IdentEntity<TId>
        {
            GetRepository<T>().Set(entity);
        }

        public void Delete<T>(TId id) where T : IdentEntity<TId>
        {
            (GetRepository<T>()).Delete(id);
        }

        public void Delete<T>(T entity) where T : IdentEntity<TId>
        {
            GetRepository<T>().Delete(entity);
        }

        public Criteria<TCriteria> Criteria<T, TCriteria>() where T: IdentEntity<TId> where TCriteria : ICriteria
        {
            return (GetRepository<T>()).Criteria<TCriteria>();
        }
        

        public void Dispose()
        {
            
        }

        
    }

    
}
