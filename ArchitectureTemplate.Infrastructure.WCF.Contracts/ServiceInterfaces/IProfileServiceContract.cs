using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces
{
    //[ServiceContract(SessionMode = SessionMode.Allowed)]
    [ServiceContract]
    public interface IProfileServiceContract
    {
        [OperationContract]
        ProfileContract GetById(int id);

        [OperationContract]
        ProfileContract GetByName(string name);

        [OperationContract]
        IEnumerable<ProfileContract> GetAll();
    }
}
