using System;
using System.Collections.Generic;
using opcode4.core.Data.Criterias;
using opcode4.core.Model;

namespace opcode4.core.Data
{
    public interface IDAOBase : ITransaction, IDisposable
    {
        void OpenConnection(string connectionString);
    }

    public interface ICustomDAO : IDisposable
    {
        void Configure();
        T Get<T>(object id);
        IList<T> GetAll<T>() where T: class; 
        void Set(object entity);
        void Delete<T>(object id);
        void Delete(object entity);
        Criteria<TCriteria> Criteria<T, TCriteria>() where TCriteria : ICriteria;
        void InitilizeObject(object entity);
        void Flush();
        void DetachObject(object entity);

        void StartTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }

    public interface IGDAO<Tid> : IDisposable
    {
        T Get<T>(Tid id) where T : IdentEntity<Tid>;
        void Set<T>(T entity) where T : IdentEntity<Tid>;
        void Delete<T>(Tid id) where T : IdentEntity<Tid>;
        void Delete<T>(T entity) where T : IdentEntity<Tid>;
        Criteria<TCriteria> Criteria<T, TCriteria>() where T : IdentEntity<Tid> where TCriteria : ICriteria;
    }

    public interface IGDAO : IDAOBase
    {
        T Get<T>(object id) where T : IdentEntityBase;
        void Set<T>(T entity) where T : IdentEntityBase;
        void Delete<T>(object id) where T : IdentEntityBase;
        void Delete<T>(T entity) where T : IdentEntityBase;
        Criteria<TCriteria> Criteria<T, TCriteria>() where T : IdentEntityBase where TCriteria : ICriteria;
    }

    public interface IDAO : IDAOBase
    {
        T Get<T>(ulong id) where T : TEntity;
        void Set<T>(T entity) where T : TEntity;
        void Delete<T>(ulong id) where T : TEntity;
        void Delete<T>(T entity) where T : TEntity;
        Criteria<TCriteria> Criteria<T, TCriteria>() where T : IdentNULongEntity where TCriteria : ICriteria;
    }

    public interface ISDAO : IDAOBase
    {
        T Get<T>(string id) where T : IdentStringEntity;
        void Set<T>(T entity) where T : IdentStringEntity;
        void Delete<T>(string id) where T : IdentStringEntity;
        void Delete<T>(T entity) where T : IdentStringEntity;
        Criteria<TCriteria> Criteria<T, TCriteria>() where T : IdentStringEntity where TCriteria : ICriteria;
    }
}
