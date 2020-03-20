using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IContaService
    {
        Task<IEnumerable<Conta>> ObterContas();
        Task<Conta> ObterPorId(int id);
        void Salvar(Conta conta);
        void Excluir(Conta conta);
        IEnumerable<ContaExtrato> ObterContaExtrato();
        IEnumerable<ListaConta> ObterListaConta();
    }
}
