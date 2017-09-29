using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface IEmailMailService
    {
        void SendEmail(EmailMail entity);
    }
}
