using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class LogType
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Log> Log { get; set; }
    }
}
