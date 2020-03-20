using Gomi.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Services
{
    public class CaixaService : ICaixaService
    {
        private readonly ICaixaRepository _caixaRepository;

        public CaixaService(ICaixaRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
        }

        public async Task<IEnumerable<Caixa>> ObterMovimentosCaixa()
        {
            return await _caixaRepository.GetAll();
        }

        public async Task<Caixa> ObterPorId(int id)
        {
            return await _caixaRepository.GetById(id);
        }

        public void Salvar(Caixa caixa)
        {
            if (caixa.Id == 0)
                _caixaRepository.Add(caixa);
            else
                _caixaRepository.Update(caixa);
        }

        public void Excluir(Caixa caixa)
        {
            _caixaRepository.Delete(caixa);
        }
    }
}
