using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPlanoContaRepository : IRepository<PlanoConta>
    {
        IEnumerable<ListaCategoria> ObterListaCategoria();
    }
}
