using System;

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions
{
    public class Pagination
    {
        public Pagination()
        {
            QtdeItensPagina = 10;
        }

        public int QtdeItensPagina { get; set; }

        public int QtdePaginas { get; set; }

        public int PaginaAtual { get; set; }

        public int SkipPagina(Pagination entity)
        {
            return entity.PaginaAtual > 1
                ? (entity.PaginaAtual - 1) * entity.QtdeItensPagina : 0;
        }

        public Pagination CalculatePaging(Pagination entity, int count)
        {
            return new Pagination
            {
                QtdePaginas = (int)Math.Ceiling(count / Convert.ToDouble(entity.QtdeItensPagina)),
                PaginaAtual = entity.PaginaAtual != 0 ? entity.PaginaAtual : 1
            };
        }

        public string GeneretePaginationSql(Pagination entity, string orderBy, bool desc = false)
        {
            return $"ORDER BY {orderBy} " + (desc ? "DESC " : "ASC ") +
                   $"OFFSET {entity.SkipPagina(entity)} " +
                   $"ROW FETCH NEXT {entity.QtdeItensPagina} ROWS ONLY";
        }
    }
}
