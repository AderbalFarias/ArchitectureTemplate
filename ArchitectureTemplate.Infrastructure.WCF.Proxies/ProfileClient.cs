using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ArchitectureTemplate.Infrastructure.WCF.Proxies
{
    public class ProfileClient : ClientBase<IProfileServiceContract>, IProfileServiceContract
    {
        public ProfileClient(string endpointName)
            : base(endpointName)
        {
        }

        public ProfileClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public ProfileContract GetById(int id)
        {
            return Channel.GetById(id);
        }

        public ProfileContract GetByName(string name)
        {
            return Channel.GetByName(name);
        }

        public IEnumerable<ProfileContract> GetAll()
        {
            return Channel.GetAll();
        }
    }
}
