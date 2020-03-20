using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface ICentroCustoService
    {
        Task<IEnumerable<CentroCusto>> ObterCentrosCustos();
        void Salvar(CentroCusto centroCusto);
        void Excluir(CentroCusto centroCusto);
    }
}
