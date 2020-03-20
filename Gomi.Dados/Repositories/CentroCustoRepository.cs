using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Dados.Repositories
{
    public class CentroCustoRepository : Repository<CentroCusto>, ICentroCustoRepository
    {
        public CentroCustoRepository(IDapperContext dapperContext) : base(dapperContext)
        { }
    }
}
