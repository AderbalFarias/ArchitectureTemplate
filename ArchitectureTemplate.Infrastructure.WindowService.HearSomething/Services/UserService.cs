using ArchitectureTemplate.Domain.Interfaces.Services;

namespace ArchitectureTemplate.Infrastructure.WindowService.HearSomething.Services
{
    public class UserService
    {
        private readonly IUserService _userService;

        public UserService(IUserService userService)
        {
            _userService = userService;
        }

        public void Action()
        {
            var getUsers = _userService.GetAll();
        }
    }
}
