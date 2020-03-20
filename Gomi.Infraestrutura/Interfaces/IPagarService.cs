using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPagarService
    {
        Task<IEnumerable<Pagar>> ObterPagamentos();
        void Salvar(Pagar pagamento);
        void Excluir(Pagar pagamento);
        Pagar ObterPorId(int id);
    }
}
