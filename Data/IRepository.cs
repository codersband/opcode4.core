using opcode4.core.Data.Criterias;
using opcode4.core.Model;

namespace opcode4.core.Data
{
    public interface ICriteriaRepository
    {
        Criteria<TCriteria> Criteria<TCriteria>() where TCriteria : ICriteria;
    }

    public interface IRepository<TEntity, in TId> : ICriteriaRepository where TEntity : IdentEntity<TId>
    {
        TEntity Get(TId id);
        void Set(TEntity entity);
        void Delete(TId id);
        void Delete(TEntity entity);
    }

    public interface IRepository<TEntity> : ICriteriaRepository where TEntity : IdentNULongEntity
    {
        TEntity Get(ulong id);
        void Set(TEntity entity);
        void Delete(ulong id);
        void Delete(TEntity entity);
    }

    public interface IGRepository<TEntity> : ICriteriaRepository where TEntity : IdentEntityBase
    {
        TEntity Get(object id);
        void Set(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}