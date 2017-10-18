using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ArchitectureTemplate.Infrastructure.WCF.Proxies
{
    public class ScreenClient : ClientBase<IScreenServiceContract>, IScreenServiceContract
    {
        public ScreenClient(string endpointName)
            : base(endpointName)
        {
        }

        public ScreenClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public ScreenContract GetById(int id)
        {
            return Channel.GetById(id);
        }

        public ScreenContract GetByName(string name)
        {
            return Channel.GetByName(name);
        }

        public IEnumerable<ScreenContract> GetScreens(string key)
        {
            return Channel.GetScreens(key);
        }

        public IEnumerable<ScreenContract> GetScreens(int idBegin, int idEnd)
        {
            return Channel.GetScreens(idBegin, idEnd);
        }
    }
}
