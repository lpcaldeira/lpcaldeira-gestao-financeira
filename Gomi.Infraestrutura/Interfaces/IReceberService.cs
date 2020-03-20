using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IReceberService
    {
        Task<IEnumerable<Receber>> ObterRecebimentos();
        void Salvar(Receber recebimento);
        void Excluir(Receber recebimento);
        Receber ObterPorId(int id);
    }
}
