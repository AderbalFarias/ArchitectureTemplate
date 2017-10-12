using System.Collections.Generic;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
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

        public IEnumerable<Log> Get(Pagination paginar, long? testId = null, string key = null)
        {
            return _logRepository.Get(paginar, testId, key);
        }

        public int Count(long? testId = null, string key = null)
        {
            return _logRepository.Count(testId, key);
        }

        #endregion
    }
}