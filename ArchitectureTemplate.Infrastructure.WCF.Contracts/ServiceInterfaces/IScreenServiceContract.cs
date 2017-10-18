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

        [OperationContract(Name = "GetScreensByContains")]
        IEnumerable<ScreenContract> GetScreens(string key);

        [OperationContract(Name = "GetScreensByRange")]
        IEnumerable<ScreenContract> GetScreens(int idBegin, int idEnd);
    }
}
