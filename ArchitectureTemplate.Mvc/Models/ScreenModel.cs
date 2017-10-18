using ArchitectureTemplate.Mvc.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Mvc;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class ScreenModel
    {
        [DisplayName(@"Identifier")]
        public int Id { get; set; }

        [DisplayName(@"Name")]
        [Required(ErrorMessage = @"Field Required")]
        [StringLength(50, ErrorMessage = @"Field must be 50 characters or less")]
        public string Nome { get; set; }

        [DisplayName(@"Controller")]
        public string ControllerName { get; set; }

        [DisplayName(@"Date of Create")]
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        [DisplayName(@"Cread")]
        public bool Create { get; set; }

        [DisplayName(@"Read")]
        public bool Read { get; set; }

        [DisplayName(@"Update")]
        public bool Update { get; set; }

        [DisplayName(@"Delete")]
        public bool Delete { get; set; }

        //public ICollection<ProfilePorScreen> ProfilePorScreen { get; set; }

        public IEnumerable<object> MapperActions()
        {
            var actionList = Assembly.GetAssembly(typeof(MvcApplication))
                .GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && !type.Name.Equals("CustomController"))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => m.IsPublic && !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
                .Select(x => new
                {
                    Controller = x.DeclaringType?.Name,
                    Action = x.Name,
                    ReturnType = x.ReturnParameter,
                    AttributesAll = string.Join(",", x.GetCustomAttributes()
                        .Select(a => a.GetType().Name.Replace("Attribute", "")))
                    //AttributesVerbs = string.Join(",", x.GetCustomAttributes<AcceptVerbsAttribute>()
                    //    .SelectMany(s => s.Verbs).ToList())
                })
                .OrderBy(x => x.Controller)
                .ThenBy(x => x.Action)
                .ToList();

            return actionList;
        }

        public IList<Screen> MapperControllers()
        {
            var actionList = Assembly.GetAssembly(typeof(MvcApplication))
                .GetTypes()
                .Where(type => (typeof(Controller).IsAssignableFrom(type)
                    || typeof(ApiController).IsAssignableFrom(type))
                        && !type.Name.Equals("CustomController") && type.IsPublic
                        && !type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
                .Select(s => new Screen
                {
                    Nome = s.Name.Replace("Controller", ""),
                    ControllerName = s.Name,
                    DataCadastro = DateTime.Now,
                    Ativo = true,
                    Create = GetAccessType(s.GetMethods(BindingFlags.Instance
                        | BindingFlags.DeclaredOnly | BindingFlags.Public), AccessType.Create),
                    Read = GetAccessType(s.GetMethods(BindingFlags.Instance
                        | BindingFlags.DeclaredOnly | BindingFlags.Public), AccessType.Read),
                    Update = GetAccessType(s.GetMethods(BindingFlags.Instance
                        | BindingFlags.DeclaredOnly | BindingFlags.Public), AccessType.Update),
                    Delete = GetAccessType(s.GetMethods(BindingFlags.Instance
                        | BindingFlags.DeclaredOnly | BindingFlags.Public), AccessType.Delete),
                })
                .OrderBy(x => x.ControllerName)
                .ToList();

            return actionList;
        }

        private bool GetAccessType(IEnumerable<MethodInfo> methods, AccessType accessType)
        {
            return methods.SelectMany(sm => sm.GetCustomAttributes<ActionType>()
                .Select(s => s.AccessType)).Any(w => w == accessType);
        }
    }
}
