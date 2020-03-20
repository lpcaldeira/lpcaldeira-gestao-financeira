using System;
using Gomi.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gomi.Infraestrutura.Models.Financeiro;

namespace Gomi.Infraestrutura.Services
{
    public class PagarService : IPagarService
    {
        private readonly IPagarRepository _pagarRepository;
        private readonly ICaixaService _caixaService;

        public PagarService(IPagarRepository pagarRepository,
            ICaixaService caixaService)
        {
            _pagarRepository = pagarRepository;
            _caixaService = caixaService;
        }

        public async Task<IEnumerable<Pagar>> ObterPagamentos()
        {
            var resultado = _pagarRepository.ObterPagamentos();
            return await Task.Factory.StartNew(() => resultado);
        }

        public async void Salvar(Pagar pagamento)
        {
            if (pagamento.IdContaPagamento == 0)
            {
                pagamento.DataPagamento = null;
                pagamento.IdContaPagamento = null;
                pagamento.ValorPago = 0;
            }

            SalvarPagar(pagamento);

            if (pagamento.ValorPago == 0 || pagamento.IdContaPagamento == null) return;
            if (pagamento.DataPagamento == null || pagamento.DataPagamento == DateTime.MinValue)
                pagamento.DataPagamento = DateTime.Now;

            var lancamentoCaixa = await _caixaService.ObterPorId(pagamento.IdCaixa ?? 0) ?? new Caixa();
            lancamentoCaixa.DataCompetencia = pagamento.DataPagamento ?? DateTime.Now;
            lancamentoCaixa.IdConta = pagamento.IdContaPagamento ?? 0;
            lancamentoCaixa.NomeConta = pagamento.NomeContaPagamento;
            lancamentoCaixa.IdEmpresa = pagamento.IdEmpresa;
            lancamentoCaixa.IdCentroCusto = pagamento.IdCentroCusto;
            lancamentoCaixa.DescricaoCentroCusto = pagamento.DescricaoCentroCusto;
            lancamentoCaixa.IdPlanoConta = pagamento.IdPlanoConta;
            lancamentoCaixa.DescricaoPlanoConta = pagamento.DescricaoPlanoConta;
            lancamentoCaixa.Descricao = "Lançamento de pagamento";
            lancamentoCaixa.Debito = pagamento.ValorPago;

            _caixaService.Salvar(lancamentoCaixa);
            pagamento.IdCaixa = lancamentoCaixa.Id;

            SalvarPagar(pagamento);
        }

        private void SalvarPagar(Pagar pagamento)
        {
            if (pagamento.Id == 0)
                _pagarRepository.Add(pagamento);
            else
                _pagarRepository.Update(pagamento);
        }

        public void Excluir(Pagar pagamento)
        {
            if (pagamento.Caixa != null)
                _caixaService.Excluir(pagamento.Caixa);
            _pagarRepository.Delete(pagamento);
        }

        public Pagar ObterPorId(int id)
        {
            return _pagarRepository.GetById(id).Result;
        }
    }
}
