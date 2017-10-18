using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ArchitectureTemplate.Infrastructure.WCF.Proxies
{
    public class TelaClient : ClientBase<IScreenServiceContract>, IScreenServiceContract
    {
        public TelaClient(string endpointName)
            : base(endpointName)
        {
        }

        public TelaClient(Binding binding, EndpointAddress address)
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

        public IEnumerable<ScreenContract> GetTelas(string key)
        {
            return Channel.GetTelas(key);
        }

        public IEnumerable<ScreenContract> GetTelas(int idBegin, int idEnd)
        {
            return Channel.GetTelas(idBegin, idEnd);
        }
    }
}
