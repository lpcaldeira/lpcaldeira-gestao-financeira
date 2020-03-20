using Gomi.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Services
{
    public class PlanoContaService : IPlanoContaService
    {
        private readonly IPlanoContaRepository _planoContaRepository;

        public PlanoContaService(IPlanoContaRepository planoContaRepository)
        {
            _planoContaRepository = planoContaRepository;
        }

        public async Task<IEnumerable<PlanoConta>> ObterPlanosContas()
        {
            return await _planoContaRepository.GetAll();
        }

        public async Task<IEnumerable<PlanoConta>> ObterCategoriaPorNome(string categoria)
        {
            return await _planoContaRepository.GetAll();
        }

        public async Task<IEnumerable<PlanoConta>> ObterPlanosContasSaida()
        {
            var resultado = await _planoContaRepository.GetAll();
            return resultado.Where(x => x.TipoMovimentoValor == TipoMovimentoPlanoConta.Saida);
        }

        public async Task<IEnumerable<PlanoConta>> ObterPlanosContasEntrada()
        {
            var resultado = await _planoContaRepository.GetAll();
            return resultado.Where(x => x.TipoMovimentoValor == TipoMovimentoPlanoConta.Entrada);
        }

        public void Salvar(PlanoConta planoConta)
        {
            if (planoConta.Id == 0)
                _planoContaRepository.Add(planoConta);
            else
                _planoContaRepository.Update(planoConta);
        }

        public void Excluir(PlanoConta planoConta)
        {
            _planoContaRepository.Delete(planoConta);
        }
    }
}
