using Gomi.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Services
{
    public class CentroCustoService : ICentroCustoService
    {
        private readonly ICentroCustoRepository _centroCustoRepository;

        public CentroCustoService(ICentroCustoRepository centroCustoRepository)
        {
            _centroCustoRepository = centroCustoRepository;
        }

        public async Task<IEnumerable<CentroCusto>> ObterCentrosCustos()
        {
            return await _centroCustoRepository.GetAll();
        }

        public void Salvar(CentroCusto centroCusto)
        {
            if (centroCusto.Id == 0)
                _centroCustoRepository.Add(centroCusto);
            else
                _centroCustoRepository.Update(centroCusto);
        }

        public void Excluir(CentroCusto centroCusto)
        {
            _centroCustoRepository.Delete(centroCusto);
        }
    }
}
