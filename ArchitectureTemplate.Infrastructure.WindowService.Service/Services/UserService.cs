using ArchitectureTemplate.Business.Interfaces.Services;

namespace ArchitectureTemplate.Infrastructure.WindowService.Service.Services
{
    public class UserService
    {
        private readonly IUsuarioService _usuarioService;

        public UserService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public void Action()
        {
            var getUsers = _usuarioService.GetAll();
        }
    }
}
