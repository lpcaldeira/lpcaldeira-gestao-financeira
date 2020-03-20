using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Financeiro.Dashboard;
using System;

namespace Gomi.Infraestrutura.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly ICaixaService _caixaService;
        private readonly IEmpresaService _empresaService;

        public ContaService(IContaRepository contaRepository,
            ICaixaService caixaService,
            IEmpresaService empresaService)
        {
            _contaRepository = contaRepository;
            _caixaService = caixaService;
            _empresaService = empresaService;
        }

        public async Task<IEnumerable<Conta>> ObterContas()
        {
            return await _contaRepository.GetAll();
        }

        public async Task<Conta> ObterPorId(int id)
        {
            return await _contaRepository.GetById(id);
        }

        public async void Salvar(Conta conta)
        {
            SalvarConta(conta);

            var saldo = conta.SaldoInicial;
            if (saldo == 0) return;

            var lancamentoCaixa = await _caixaService.ObterPorId(conta.IdCaixa ?? 0) ?? new Caixa();
            var empresa = _empresaService.ObterEmpresas().Result.FirstOrDefault();
            lancamentoCaixa.DataCompetencia = conta.DataSaldoInicial;
            lancamentoCaixa.IdConta = conta.Id;
            lancamentoCaixa.NomeConta = conta.Nome;
            lancamentoCaixa.IdEmpresa = empresa.Id;
            lancamentoCaixa.Descricao = "Lançamento de saldo inicial da conta";

            if (saldo > 0)
                lancamentoCaixa.Credito = saldo;
            else if (saldo < 0)
                lancamentoCaixa.Debito = saldo;

            _caixaService.Salvar(lancamentoCaixa);
            conta.IdCaixa = lancamentoCaixa.Id;

            SalvarConta(conta);
        }

        private void SalvarConta(Conta conta)
        {
            if (conta.Id == 0)
                _contaRepository.Add(conta);
            else
                _contaRepository.Update(conta);
        }

        public void Excluir(Conta conta)
        {
            _contaRepository.Delete(conta);
        }

        public IEnumerable<ContaExtrato> ObterContaExtrato()
        {

            return _contaRepository.ObterContaExtrato();
        }

        public IEnumerable<ListaConta> ObterListaConta()
        {

            return _contaRepository.ObterListaConta();
        }
    }
}
