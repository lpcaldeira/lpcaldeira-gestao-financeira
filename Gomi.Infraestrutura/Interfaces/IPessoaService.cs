using Gomi.Infraestrutura.Models.Pessoas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gomi.Infraestrutura.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> ObterPorId(int id);
        Task<IEnumerable<Pessoa>> ObterPessoas();
        Task<IEnumerable<Pessoa>> ObterPessoasParaPagamentoPorNome(string nome);
        Task<IEnumerable<Pessoa>> ObterPessoasParaRecebimentoPorNome(string nome);
        void Salvar(Pessoa pessoa);
        void Excluir(Pessoa pessoa);
        IEnumerable<ListaPessoas> ObterListaPessoas();
        IEnumerable<ListaPessoasDetalhado> ObterListaPessoasDetalhado();
        IEnumerable<ListaMovimentacaoHistorico> ObterListaMovimentacaoHistorico();
        object ObterPessoasParaRecebimentoPorNome();
    }
}
