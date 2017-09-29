using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;

namespace ArchitectureTemplate.Business.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        #region Fields

        private readonly IRepositoryBase<TEntity> _repository;

        #endregion

        #region Constructors

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public void Add(TEntity objModel, long userId)
        {
            _repository.Add(objModel, userId);
        }

        public object Add(TEntity objModel, long userId, string property)
        {
            return _repository.Add(objModel, userId, property);
        }

        public void AddRange(IList<TEntity> objModel, long userId)
        {
            _repository.AddRange(objModel, userId);
        }

        public TEntity GetId(long id)
        {
            return _repository.GetId(id);
        }

        public async Task<TEntity> GetIdAsync(long id)
        {
            return await _repository.GetIdAsync(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetAsync(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.GetList(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetAllAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public int Count()
        {
            return _repository.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public void Update(TEntity objModel, long userId, bool openContextInRequest = false, bool referenceCircular = false)
        {
            _repository.Update(objModel, userId, openContextInRequest, referenceCircular);
        }

        public void UpdateWithCollection(TEntity objModel, long userId, IEnumerable<Type> collectionList = null, IEnumerable<Type> objList = null)
        {
            _repository.UpdateWithCollection(objModel, userId, collectionList, objList);
        }

        public void Remove(TEntity objModel, long userId)
        {
            _repository.Remove(objModel, userId);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        #endregion
    }
}
