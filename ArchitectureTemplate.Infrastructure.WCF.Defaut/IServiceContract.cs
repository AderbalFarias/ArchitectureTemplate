using ArchitectureTemplate.Infrastructure.WCF.Default.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Default
{
    [ServiceContract]
    public interface IServiceContract
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
