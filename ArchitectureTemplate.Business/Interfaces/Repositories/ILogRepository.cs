using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;

namespace ArchitectureTemplate.Business.Interfaces.Repositories
{
    public interface ILogRepository
    {
        void Add(Log entity);
        IEnumerable<Log> Get();
        IEnumerable<Log> Get(Pagination paginar, long? testId = null, string key = null);
        int Count(long? testId = null, string key = null);
    }
}
