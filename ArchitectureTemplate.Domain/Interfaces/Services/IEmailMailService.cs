using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IEmailMailService
    {
        void SendEmail(EmailMail entity);
    }
}
