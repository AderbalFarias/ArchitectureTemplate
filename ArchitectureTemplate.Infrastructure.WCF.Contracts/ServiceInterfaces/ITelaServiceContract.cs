using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces
{
    [ServiceContract]
    public interface ITelaServiceContract
    {
        [OperationContract]
        TelaContract GetById(int id);

        [OperationContract]
        TelaContract GetByName(string name);

        [OperationContract(Name = "GetTelasByContains")]
        IEnumerable<TelaContract> GetTelas(string key);

        [OperationContract(Name = "GetTelasByRange")]
        IEnumerable<TelaContract> GetTelas(int idBegin, int idEnd);
    }
}
