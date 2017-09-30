using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface ILogService
    {
        void Add(Log entity);
        IEnumerable<Log> Get();
        IEnumerable<Log> Get(Pagination paginar, long? testId = null, string key = null);
        int Count(long? testId = null, string key = null);
    }
}
