using System.Collections.Generic;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface ITransferenciaRepository : IRepository<Transferencia>
    {
        IEnumerable<TransferenciaLista> ObterTransferencias();
    }
}