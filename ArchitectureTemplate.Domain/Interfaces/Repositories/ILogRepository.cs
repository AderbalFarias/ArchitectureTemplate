using System.Collections.Generic;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        void Add(Log entity);
        IEnumerable<Log> Get();
        IEnumerable<Log> Get(Pagination paginar, long? testId = null, string key = null);
        int Count(long? testId = null, string key = null);
    }
}
