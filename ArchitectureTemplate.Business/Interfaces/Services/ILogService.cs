using System.Collections.Generic;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface ILogService
    {
        void Add(Log entity);
        IEnumerable<Log> Get();
        IEnumerable<Log> Get(Pagination paginar, long? processoId = null, string key = null);
        int Count(long? processoId = null, string key = null);
    }
}
