using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Interface
{
    [ServiceContract]
    public interface ITelaService
    {
        [OperationContract]
        Tela GetById(int id);

        [OperationContract]
        Tela GetByName(string name);

        [OperationContract(Name = "GetTelasByContains")]
        IEnumerable<Tela> GetTelas(string key);

        [OperationContract(Name = "GetTelasByRange")]
        IEnumerable<Tela> GetTelas(int idBegin, int idEnd);
    }
}
