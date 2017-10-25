using ArchitectureTemplate.Infrastructure.WCF.Defaut.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Defaut
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
