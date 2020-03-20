using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Gomi.Dados.Versoes;
using Gomi.Infraestrutura.Models;

namespace Gomi.Dados
{
    public static class Atualizacao
    {
        internal static IDictionary<int, IEnumerable<string>> Scripts
            => new Dictionary<int, IEnumerable<string>>
        {
            { ScriptVersao1.Versao, ScriptVersao1.Scripts }
        };

        public static async void ExecutarScriptsDaVersao(this IDapperContext dbContext)
        {
            var versao = await ObterVersao(dbContext);
            foreach (var script in Scripts.Where(x => x.Key > versao.Valor).SelectMany(x => x.Value))
                dbContext.ExecuteScript(script);
            AtualizarVersao(versao, dbContext);
        }

        private static async Task<Versao> ObterVersao(IDapperContext dbContext)
        {
            var versao = await dbContext.QueryOneAsync<Versao>("select * from versao");
            return versao ?? new Versao();
        }

        private static async void AtualizarVersao(Versao versao, IDapperContext dbContext)
        {
            var ultimaVersao = Scripts.Keys.LastOrDefault();
            if (versao.Valor == ultimaVersao) return;
            versao.Valor = ultimaVersao;
            if (versao.Id == 0)
                await dbContext.InsertAsync(versao);
            else
                await dbContext.UpdateAsync(versao);
        }
    }
}