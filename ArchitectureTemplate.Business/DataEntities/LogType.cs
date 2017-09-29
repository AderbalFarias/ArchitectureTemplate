using System.Collections.Generic;

namespace ArchitectureTemplate.Business.DataEntities
{
    public class LogType
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Log> Log { get; set; }
    }
}
