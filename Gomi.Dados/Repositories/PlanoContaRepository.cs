using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Linq;

namespace Gomi.Dados.Repositories
{
    public class PlanoContaRepository : Repository<PlanoConta>, IPlanoContaRepository
    {
        public PlanoContaRepository(IDapperContext dapperContext) : base(dapperContext)
        {}

        public IEnumerable<ListaCategoria> ObterListaCategoria()
        {
            var resultado = DbContext.Query("select id, descricao, tipomovimento, categoria from planoconta").ToList();
            return (from registro in resultado
                    select new ListaCategoria(registro.id, registro.descricao, registro.tipomovimento, registro.categoria)).ToList();
        }

    }
}
