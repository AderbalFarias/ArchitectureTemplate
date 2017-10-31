using ArchitectureTemplate.Infrastructure.WCF.Default.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.WCF.Default
{
    [ServiceContract]
    public interface IServiceContract
    {

        [OperationContract]
        ScreenContract GetById(int id);

        [OperationContract]
        Task<ScreenContract> GetByIdAsync(int id);

        [OperationContract]
        ScreenContract GetByName(string name);

        [OperationContract]
        Task<ScreenContract> GetByNameAsync(string name);

        [OperationContract]
        IEnumerable<ScreenContract> GetScreens(string key);

        [OperationContract]
        Task<IEnumerable<ScreenContract>> GetScreensAsync(string key);
    }
}
