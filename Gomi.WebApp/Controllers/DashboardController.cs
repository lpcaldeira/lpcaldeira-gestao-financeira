using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;
using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using DevExtreme.AspNet.Mvc;
using Gomi.Infraestrutura.Enums;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gomi.WebApp.Controllers

{
    public class DashboardController : Controller
    {
        private readonly IPessoaService _pessoaService;
        private readonly IEmpresaService _empresaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICentroCustoService _centroCustoService;
        private readonly IPlanoContaService _PlanoContaService;
        private readonly IContaService _contaService;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IReceberRepository _receberRepository;
        private readonly IPagarRepository _pagarRepository;

        public DashboardController(IPessoaService pessoaService,
            IEmpresaService empresaService,
            IUsuarioService usuarioService,
            ICentroCustoService centroCustoService,
            IPlanoContaService PlanoContaService,
            IContaService contaService,
            ICaixaRepository caixaRepository,
            IReceberRepository receberRepository,
            IPagarRepository pagarRepository
            )
        {
            _pessoaService = pessoaService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _centroCustoService = centroCustoService;
            _PlanoContaService = PlanoContaService;
            _contaService = contaService;
            _caixaRepository = caixaRepository;
            _receberRepository = receberRepository;
            _pagarRepository = pagarRepository;

        }
        public IActionResult Principal()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ObterListaClientes(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Cliente");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Receber")), "application/json");
        }
        

        [HttpPost]
        public ActionResult ObterClientesDetalhado([FromBody] ListaPessoasDetalhado cliente)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == cliente.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Receber")), "application/json");
        }

        [HttpPost]

        public ActionResult ObterHistoricoClientesMovimentacao([FromBody] ListaMovimentacaoHistorico cliente)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == cliente.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Receber")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterClientesInformacao([FromBody] ListaPessoas cliente)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == cliente.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Receber")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaFornecedores(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Fornecedor");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterFornecedoresDetalhado([FromBody] ListaPessoasDetalhado fornecedor)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == fornecedor.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoFornecedoresMovimentacao([FromBody] ListaMovimentacaoHistorico fornecedor)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == fornecedor.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterFornecedoresInformacao([FromBody] ListaPessoas fornecedor)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == fornecedor.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaFuncionarios(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Funcionário");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterFuncionariosDetalhado([FromBody] ListaPessoasDetalhado Funcionario)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Funcionario.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoFuncionariosMovimentacao([FromBody] ListaMovimentacaoHistorico Funcionario)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Funcionario.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterFuncionariosInformacao([FromBody] ListaPessoas Funcionario)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == Funcionario.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaSociosPagar(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Sócio");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaSociosReceber(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Sócio");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Receber")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterSociosDetalhadoPagar([FromBody] ListaPessoasDetalhado Socio)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Socio.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterSociosDetalhadoReceber([FromBody] ListaPessoasDetalhado Socio)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Socio.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Receber")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoSociosMovimentacaoPagar([FromBody] ListaMovimentacaoHistorico Socio)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Socio.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }

        [HttpPost]
        public ActionResult ObterHistoricoSociosMovimentacaoReceber([FromBody] ListaMovimentacaoHistorico Socio)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Socio.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Receber")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterSociosInformacao([FromBody] ListaPessoas Socio)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == Socio.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaGovernos(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Governo");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterGovernosDetalhado([FromBody] ListaPessoasDetalhado Governo)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Governo.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoGovernosMovimentacao([FromBody] ListaMovimentacaoHistorico Governo)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Governo.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterGovernosInformacao([FromBody] ListaPessoas Governo)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == Governo.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaFinanciadores(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Financiador");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterFinanciadoresDetalhado([FromBody] ListaPessoasDetalhado Financiador)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Financiador.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoFinanciadoresMovimentacao([FromBody] ListaMovimentacaoHistorico Financiador)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Financiador.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterFinanciadoresInformacao([FromBody] ListaPessoas Financiador)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == Financiador.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpGet]
        public ActionResult ObterListaBancos(DataSourceLoadOptions options)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Transacionador == "Banco");
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterBancosDetalhado([FromBody] ListaPessoasDetalhado Banco)
        {
            var pessoasdetalhado = _pessoaService.ObterListaPessoasDetalhado().Where(e => e.Id == Banco.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }

        [HttpPost]
        public ActionResult ObterHistoricoBancosMovimentacao([FromBody] ListaMovimentacaoHistorico Banco)
        {
            var pessoasdetalhado = _pessoaService.ObterListaMovimentacaoHistorico().Where(e => e.Id == Banco.Id);
            return Content(JsonConvert.SerializeObject(pessoasdetalhado.ToList().Where(e => e.Tipo == "Pagar")), "application/json");

        }


        [HttpPost]
        public ActionResult ObterBancosInformacao([FromBody] ListaPessoas banco)
        {
            var pessoas = _pessoaService.ObterListaPessoas().Where(e => e.Id == banco.Id);
            return Content(JsonConvert.SerializeObject(pessoas.ToList().Where(e => e.Tipo == "Pagar")), "application/json");
        }


        [HttpGet]
        public ActionResult ObterListaContas(DataSourceLoadOptions options)
        {
            var contas = _contaService.ObterListaConta();
            return Content(JsonConvert.SerializeObject(contas.ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult ObterSaldoDisponibilidade(DataSourceLoadOptions options)
        {
            var saldo = _caixaRepository.ObterSaldosDisponibilidade();
            return Content(JsonConvert.SerializeObject(saldo.ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult ObterRecebimentosRecebido(DataSourceLoadOptions options)
        {
            var receber = _receberRepository.ObterListaRecebimentos().Where(e => e.Valorrecebido > 0);
            return Content(JsonConvert.SerializeObject(receber.ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult ObterRecebimentosEmAberto(DataSourceLoadOptions options)
        {
            var receber = _receberRepository.ObterListaRecebimentos().Where(e => e.Valorrecebido == 0);
            return Content(JsonConvert.SerializeObject(receber.ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult ObterPagamentosPagos(DataSourceLoadOptions options)
        {
            var pagar = _pagarRepository.ObterListaPagamentos().Where(e => e.Valorpago > 0);
            return Content(JsonConvert.SerializeObject(pagar.ToList()), "application/json");
        }

        [HttpGet]
        public ActionResult ObterPagamentosEmAberto(DataSourceLoadOptions options)
        {
            var pagar = _pagarRepository.ObterListaPagamentos().Where(e => e.Valorpago == 0);
            return Content(JsonConvert.SerializeObject(pagar.ToList()), "application/json");
        }
        

        [HttpGet]
        public ActionResult ObterCategorias(DataSourceLoadOptions loadOptions)
        {
            var filtro = loadOptions.Filter;
            var informacaoPesquisa = string.Empty;
            if (filtro != null)
            {
                informacaoPesquisa = filtro.Count == 1
                    ? ((JArray)filtro[0])?.Last?.Value<string>() ?? string.Empty
                    : ((List<object>)filtro).LastOrDefault() as string ?? string.Empty;
            }

            var categoria = _PlanoContaService.ObterCategoriaPorNome(informacaoPesquisa.ToLowerInvariant()).Result; 
            return Content(JsonConvert.SerializeObject(categoria.ToList()), "application/json");
        }

   
    }
    
}