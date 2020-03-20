using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface ICaixaService
    {
        Task<IEnumerable<Caixa>> ObterMovimentosCaixa();
        Task<Caixa> ObterPorId(int id);
        void Salvar(Caixa caixa);
        void Excluir(Caixa caixa);
    }
}
