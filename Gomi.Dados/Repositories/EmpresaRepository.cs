using Dapper.Core.Contexts.Interfaces;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Pessoas;

namespace Gomi.Dados.Repositories
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(IDapperContext dapperContext) : base(dapperContext)
        { }
    }
}
