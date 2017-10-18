using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces
{
    [ServiceContract]
    public interface IScreenServiceContract
    {
        [OperationContract]
        ScreenContract GetById(int id);

        [OperationContract]
        ScreenContract GetByName(string name);

        [OperationContract(Name = "GetTelasByContains")]
        IEnumerable<ScreenContract> GetTelas(string key);

        [OperationContract(Name = "GetTelasByRange")]
        IEnumerable<ScreenContract> GetTelas(int idBegin, int idEnd);
    }
}
