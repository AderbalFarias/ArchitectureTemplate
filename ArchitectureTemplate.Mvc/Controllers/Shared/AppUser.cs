using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ArchitectureTemplate.Mvc.Controllers.Shared
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public long UserId => Convert.ToInt64(FindFirst(ClaimTypes.NameIdentifier).Value);

        public string Country => FindFirst(ClaimTypes.Country).Value ?? string.Empty;

        public string Email => FindFirst(ClaimTypes.Email).Value;
        public long Cpf => Convert.ToInt64(FindFirst("Cpf").Value);

        public string PerfilName => FindFirst(ClaimTypes.Role).Value;

        public string Login => FindFirst("Login").Value;

        public int PerfilId => Convert.ToInt32(FindFirst("PerfilId").Value);

        public long? HierarquiaId => Convert.ToInt64(FindFirst("HierarquiaId").Value);

        public IEnumerable<int> IdsMenu => FindFirst("IdsMenu").Value?.Split('|').Select(int.Parse);

        public IEnumerable<long> IdsHierarquia => FindFirst("IdsHierarquia").Value?.Split('|').Select(long.Parse);

        public string Token => FindFirst("Token").Value;
    }
}