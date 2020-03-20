using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface ITransferenciaService
    {
        IEnumerable<TransferenciaLista> ObterTransferencias();
        Task<Transferencia> ObterPorId(int id);
        void Salvar(Transferencia transferencia);
        void Excluir(Transferencia transferencia);
    }
}