using System.Collections.Generic;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Services
{
    public class LogService : ILogService
    {
        #region Fields

        private readonly ILogRepository _logRepository;

        #endregion

        #region Constructors

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        #endregion

        #region Methods
        
        public void Add(Log entity)
        {
            _logRepository.Add(entity);
        }

        public IEnumerable<Log> Get()
        {
            return _logRepository.Get();
        }

        public IEnumerable<Log> Get(Pagination paginar, long? processoId = null, string key = null)
        {
            return _logRepository.Get(paginar, processoId, key);
        }

        public int Count(long? processoId = null, string key = null)
        {
            return _logRepository.Count(processoId, key);
        }

        #endregion
    }
}