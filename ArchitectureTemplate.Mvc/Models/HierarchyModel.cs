using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class HierarchyModel
    {
        [Key]
        public long Id { get; set; }

        [DisplayName(@"Hierarchy Superior")]
        public long? HierarchyPaiId { get; set; }

        //[DisplayName(@"Hierarchy")]
        [Required(ErrorMessage = @"Field Required")]
        [StringLength(100, ErrorMessage = @"Field must be 100 characters or less")]
        public string Nome { get; set; }

        [DisplayName(@"Description")]
        [StringLength(500, ErrorMessage = @"Field must be 500 characters or less")]
        public string Descricao { get; set; }

        [DisplayName(@"Active")]
        public bool Ativo { get; set; } = true;

        [DisplayName(@"Kind of Hierarchy")]
        public int? HierarchyTypeId { get; set; }

        public bool Vertical { get; set; }

        public IEnumerable<HierarchyModel> HierarchyDown { get; set; }

        public HierarchyModel HierarchyUp { get; set; }

        public HierarchyType HierarchyType { get; set; }

        public HierarchyDetalheModel HierarchyDetalhe { get; set; }

        //public IEnumerable<User> User { get; set; }

        public IDictionary<int, string> HierarchyTypeDictionary { get; set; }


        public string TreeHierarchy(HierarchyModel model, bool openAll)
        {
            if (model.HierarchyPaiId.HasValue)
            {
                var acoes = $"<a href=\"\\Hierarchy\\Create?idPai={model.Id}\">" +
                        $"<i class=\"fa fa-plus-square-o\"></i></a>" +
                    $"<a href=\"\\Hierarchy\\Edit\\{model.Id}\" ><i class=\"fa fa-edit\"></i></a>" +
                    $"<a onclick=\"ConfirmDialog('/Hierarchy/Delete/{model.Id}', " +
                        $"'Deseja realmente excluir a hierarchy {model.Nome}')\">" +
                            $"<i class=\"fa fa-remove\"></i></a>";

                if (model.HierarchyDown.Any())
                {
                    var ul = $"{Environment.NewLine}<li>{model.Nome} ({model.HierarchyType.Descricao}) " +
                             $"{acoes} {Environment.NewLine}<ul>";

                    ul = model.HierarchyDown
                        .Aggregate(ul, (current, item) => current + TreeHierarchy(item, openAll));

                    ul = $"{ul} {Environment.NewLine}</ul>{Environment.NewLine}</li>";
                    return ul;
                }
                else
                {
                    return $"{Environment.NewLine}<li>{model.Nome} ({model.HierarchyType.Descricao}) " +
                           $"{acoes}{Environment.NewLine}</li>";
                }
            }

            return string.Empty;
        }
    }
}
