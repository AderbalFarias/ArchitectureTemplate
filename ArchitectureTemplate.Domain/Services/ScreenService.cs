using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
{
    public class ScreenService : ServiceBase<Screen>, IScreenService
    {
        #region Fields

        private readonly IScreenRepository _screenRepository;

        #endregion
        
        #region Constructors

        public ScreenService(IScreenRepository screenRepository)
            : base(screenRepository)
        {
            _screenRepository = screenRepository;
        }

        #endregion

        #region Methods
       
        public void Synchronize(IList<Screen> entityList, long userId)
        {
            IList<Screen> screens = _screenRepository
                .GetAllWithDapper()
                .ToList();
            
            var screensNaoExistentes = entityList
                .Where(w => !screens.Select(s => s.ControllerName).Contains(w.ControllerName))
                .ToList();

            var screensDelete = screens
                .Where(w => !entityList.Select(s => s.ControllerName).Contains(w.ControllerName))
                .ToList();

            var screensExistentes = screens
                .Join(entityList, t => t.ControllerName, e => e.ControllerName, (t, e) => new {t, e})
                .Select(s => new Screen
                {
                    Id = s.t.Id,
                    Nome = s.t.Nome,
                    ControllerName = s.e.ControllerName,
                    DataCadastro = s.t.DataCadastro,
                    Ativo = s.t.Ativo,
                    Create = s.e.Create,
                    Read = s.e.Read,
                    Update = s.e.Update,
                    Delete = s.e.Delete
                })
                .ToList();

            _screenRepository.AddOrUpdate(screensExistentes, userId);

            if (screensNaoExistentes.Count > 0)
            {
                _screenRepository.AddOrUpdate(screensNaoExistentes, userId);
            }

            foreach (var screen in screensDelete)
            {
                _screenRepository.Remove(screen.Id, userId);
            }
        }

        public IEnumerable<Screen> Get(Pagination pagination)
        {
            return _screenRepository.Get(pagination);
        }

        public async Task<IEnumerable<Screen>> GetAsync(Pagination pagination)
        {
            return await _screenRepository.GetAsync(pagination);
        }

        public void Update(Screen entity, long userId)
        {
            var screen = _screenRepository.GetId(entity.Id);
            screen.Nome = entity.Nome;
            screen.Ativo = entity.Ativo;

            _screenRepository.Update(screen, userId, true);
        }

        #endregion
    }
}