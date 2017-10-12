using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ArchitectureTemplate.Infrastructure.WCF.Proxies
{
    public class TelaClient : ClientBase<ITelaServiceContract>, ITelaServiceContract
    {
        public TelaClient(string endpointName)
            : base(endpointName)
        {
        }

        public TelaClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public TelaContract GetById(int id)
        {
            return Channel.GetById(id);
        }

        public TelaContract GetByName(string name)
        {
            return Channel.GetByName(name);
        }

        public IEnumerable<TelaContract> GetTelas(string key)
        {
            return Channel.GetTelas(key);
        }

        public IEnumerable<TelaContract> GetTelas(int idBegin, int idEnd)
        {
            return Channel.GetTelas(idBegin, idEnd);
        }
    }
}
