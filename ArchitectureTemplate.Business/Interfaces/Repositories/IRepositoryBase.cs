using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Business.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity objModel, long userId);
        object Add(TEntity objModel, long userId, string property);
        void AddRange(IList<TEntity> objModel, long userId);
        TEntity GetId(long id);
        Task<TEntity> GetIdAsync(long id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(TEntity objModel, long userId, bool openContextInRequest = false, bool referenceCircular = false);
        void UpdateWithCollection(TEntity objModel, long userId, IEnumerable<Type> collectionList = null, IEnumerable<Type> objList = null);
        void Remove(TEntity objModel, long userId);
        void Dispose(); 
    }
}
