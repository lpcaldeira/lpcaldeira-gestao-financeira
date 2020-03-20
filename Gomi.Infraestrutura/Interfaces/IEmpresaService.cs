using Gomi.Infraestrutura.Models.Pessoas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IEmpresaService
    {
        Task<IEnumerable<Empresa>> ObterEmpresas();
        void Salvar(Empresa empresa);
    }
}
