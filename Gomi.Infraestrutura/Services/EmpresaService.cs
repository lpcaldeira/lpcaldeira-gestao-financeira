using Gomi.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.Pessoas;

namespace Gomi.Infraestrutura.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresas()
        {
            return await _empresaRepository.GetAll();
        }

        public void Salvar(Empresa empresa)
        {
            if (empresa.Id == 0)
                _empresaRepository.Add(empresa);
            else
                _empresaRepository.Update(empresa);
        }
    }
}
