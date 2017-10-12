using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class HierarquiaModel
    {
        [Key]
        public long Id { get; set; }

        [DisplayName(@"Hierarquia Superior")]
        public long? HierarquiaPaiId { get; set; }

        //[DisplayName(@"Hierarquia")]
        [Required(ErrorMessage = @"Field Required")]
        [StringLength(100, ErrorMessage = @"Field must be 100 characters or less")]
        public string Nome { get; set; }

        [DisplayName(@"Description")]
        [StringLength(500, ErrorMessage = @"Field must be 500 characters or less")]
        public string Descricao { get; set; }

        [DisplayName(@"Active")]
        public bool Ativo { get; set; } = true;

        [DisplayName(@"Kind of Hierarquia")]
        public int? TipoHierarquiaId { get; set; }

        public bool Vertical { get; set; }

        public IEnumerable<HierarquiaModel> HierarquiaDown { get; set; }

        public HierarquiaModel HierarquiaUp { get; set; }

        public TipoHierarquia TipoHierarquia { get; set; }

        public HierarquiaDetalheModel HierarquiaDetalhe { get; set; }

        //public IEnumerable<Usuario> Usuario { get; set; }

        public IDictionary<int, string> TipoHierarquiaDictionary { get; set; }


        public string TreeHierarquia(HierarquiaModel model, bool openAll)
        {
            if (model.HierarquiaPaiId.HasValue)
            {
                var acoes = $"<a href=\"\\Hierarquia\\Create?idPai={model.Id}\">" +
                        $"<i class=\"fa fa-plus-square-o\"></i></a>" +
                    $"<a href=\"\\Hierarquia\\Edit\\{model.Id}\" ><i class=\"fa fa-edit\"></i></a>" +
                    $"<a onclick=\"ConfirmDialog('/Hierarquia/Delete/{model.Id}', " +
                        $"'Deseja realmente excluir a hierarquia {model.Nome}')\">" +
                            $"<i class=\"fa fa-remove\"></i></a>";

                if (model.HierarquiaDown.Any())
                {
                    var ul = $"{Environment.NewLine}<li>{model.Nome} ({model.TipoHierarquia.Descricao}) " +
                             $"{acoes} {Environment.NewLine}<ul>";

                    ul = model.HierarquiaDown
                        .Aggregate(ul, (current, item) => current + TreeHierarquia(item, openAll));

                    ul = $"{ul} {Environment.NewLine}</ul>{Environment.NewLine}</li>";
                    return ul;
                }
                else
                {
                    return $"{Environment.NewLine}<li>{model.Nome} ({model.TipoHierarquia.Descricao}) " +
                           $"{acoes}{Environment.NewLine}</li>";
                }
            }

            return string.Empty;
        }
    }
}
