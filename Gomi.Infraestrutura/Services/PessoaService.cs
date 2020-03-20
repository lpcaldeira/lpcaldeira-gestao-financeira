using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Pessoas;
using System;
using System.Linq;

namespace Gomi.Infraestrutura.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<Pessoa> ObterPorId(int id)
        {
            return await _pessoaRepository.GetById(id);
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoas()
        {
            return await _pessoaRepository.GetAll();
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoasParaPagamentoPorNome(string nome)
        {
            return await _pessoaRepository.ObterPessoasParaPagamentoPorNome(nome);
        }

        public async Task<IEnumerable<Pessoa>> ObterPessoasParaRecebimentoPorNome(string nome)
        {
            return await _pessoaRepository.ObterPessoasParaRecebimentoPorNome(nome);
        }

        public void Salvar(Pessoa pessoa)
        {
            if (pessoa.Id == 0)
            {
                var pessoas = _pessoaRepository.ObterPessoaPorNomeETipoTransacionador(pessoa.Nome, pessoa.TransacionadorValor).Result;
                if (pessoas.Any())
                    throw new Exception($"{pessoa.Transacionador} com mesmo nome já foi cadastrada");

                _pessoaRepository.Add(pessoa);
            }
            else
                _pessoaRepository.Update(pessoa);
        }

        public void Excluir(Pessoa pessoa)
        {
            _pessoaRepository.Delete(pessoa);
        }

        public IEnumerable<ListaPessoas> ObterListaPessoas()
        {

            return _pessoaRepository.ObterListaPessoas();

        }

        public IEnumerable<ListaPessoasDetalhado> ObterListaPessoasDetalhado()
        {

            return _pessoaRepository.ObterListaPessoasDetalhado();

        }
        public IEnumerable<ListaMovimentacaoHistorico> ObterListaMovimentacaoHistorico()
        {
            var UltimoDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-3);
            var PrimeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).AddMonths(3);

            var dataInicial = UltimoDiaMes;
            var dataFinal = PrimeiroDiaMes;
            return _pessoaRepository.ObterListaMovimentacaoHistorico(dataInicial, dataFinal);
        }

        public object ObterPessoasParaRecebimentoPorNome()
        {
            throw new NotImplementedException();
        }
    }
}
