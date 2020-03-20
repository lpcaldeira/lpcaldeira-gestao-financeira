using Gomi.Infraestrutura.Models.Financeiro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPlanoContaService 
    {
        Task<IEnumerable<PlanoConta>> ObterPlanosContas();
        Task<IEnumerable<PlanoConta>> ObterPlanosContasSaida();
        Task<IEnumerable<PlanoConta>> ObterPlanosContasEntrada();
        Task<IEnumerable<PlanoConta>> ObterCategoriaPorNome(string categoria);
        void Salvar(PlanoConta planoConta);
        void Excluir(PlanoConta planoConta);
    }
}
