using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class Log
    {
        public long Id { get; set; }
        public int LogTypeId { get; set; }
        public int? ScreenId { get; set; }
        public long? UserId { get; set; }
        public string Mensagem { get; set; }
        public string NomeClasse { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual Screen Screen { get; set; }
        public virtual LogType LogType { get; set; }
        public virtual User User { get; set; }

        public Log GeneratedForEntity<TEntity>(long userId, TEntity entity, int logType, bool refenciaCircular = false)
        {
            string serializeJson = refenciaCircular
                ? JsonConvert.SerializeObject(entity, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    })
                : JsonConvert.SerializeObject(entity, Formatting.Indented);

            return new Log
            {
                UserId = userId,
                DataCadastro = DateTime.Now,
                Mensagem = "Registrando dados de entidades",
                NomeClasse = entity.GetType().Name,
                LogTypeId = logType,
                Conteudo = serializeJson
            };
        }

        public Log GeneratedForEntity<TEntity>(long userId, IEnumerable<TEntity> entity, int logType, bool refenciaCircular = false)
        {
            string serializeJson = refenciaCircular
                ? JsonConvert.SerializeObject(entity, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    })
                : JsonConvert.SerializeObject(entity, Formatting.Indented);

            return new Log
            {
                UserId = userId,
                DataCadastro = DateTime.Now,
                Mensagem = "Registrando dados de entidades",
                NomeClasse = typeof(TEntity).Name,
                LogTypeId = logType,
                Conteudo = serializeJson
            };
        }

        public string Where(long? testId = null, string key = null)
        {
            var where = !string.IsNullOrEmpty(key)
                ? $"(l.Conteudo like'%{key}%' or l.Mensagem like'%{key}%' " +
                  $"or l.NomeClasse like'%{key}%' or lt.Descricao like'%{key}%' " +
                  $"or u.Login like'%{key}%')"
                : "l.Id > 0";

            where = where + (testId.HasValue
                ? $" and l.Conteudo like'%\"TestId\": {testId}%'"
                : string.Empty);

            return where;
        }
    }
}
