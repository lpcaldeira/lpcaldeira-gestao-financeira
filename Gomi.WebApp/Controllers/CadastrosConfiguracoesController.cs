using Gomi.Infraestrutura.Interfaces;
using Gomi.Infraestrutura.Models.ControleAcesso;
using Gomi.Infraestrutura.Models.Financeiro;
using Gomi.Infraestrutura.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Gomi.Infraestrutura.Enums;

namespace Gomi.WebApp.Controllers
{
    public class CadastrosConfiguracoesController : Controller
    {
        private readonly IPessoaService _pessoaService;
        private readonly IEmpresaService _empresaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICentroCustoService _centroCustoService;
        private readonly IPlanoContaService _planoContaService;
        private readonly IContaService _contaService;

        public CadastrosConfiguracoesController(IPessoaService pessoaService,
            IEmpresaService empresaService,
            IUsuarioService usuarioService,
            ICentroCustoService centroCustoService,
            IPlanoContaService planoContaService,
            IContaService contaService)
        {
            _pessoaService = pessoaService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _centroCustoService = centroCustoService;
            _planoContaService = planoContaService;
            _contaService = contaService;
        }

        public IActionResult Principal()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarCliente(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaClienteLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirCliente()
        {
            ViewData["Cliente"] = new Pessoa().SetarTransacionador(Transacionador.Cliente);
            return ViewComponent("CadastroConfiguracaoPessoaClienteFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarCliente(Pessoa pessoa)
        {
            var cliente = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Cliente"] = cliente;
            return ViewComponent("CadastroConfiguracaoPessoaClienteFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirCliente(Pessoa pessoa)
        {
            var cliente = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(cliente);
            return ViewComponent("CadastroConfiguracaoPessoaClienteLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarFornecedor(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaFornecedorLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirFornecedor()
        {
            ViewData["Fornecedor"] = new Pessoa().SetarTransacionador(Transacionador.Fornecedor);
            return ViewComponent("CadastroConfiguracaoPessoaFornecedorFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarFornecedor(Pessoa pessoa)
        {
            var fornecedor = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Fornecedor"] = fornecedor;
            return ViewComponent("CadastroConfiguracaoPessoaFornecedorFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirFornecedor(Pessoa pessoa)
        {
            var fornecedor = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(fornecedor);
            return ViewComponent("CadastroConfiguracaoPessoaFornecedorLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarFuncionario(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaFuncionarioLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirFuncionario()
        {
            ViewData["Funcionario"] = new Pessoa().SetarTransacionador(Transacionador.Funcionario);
            return ViewComponent("CadastroConfiguracaoPessoaFuncionarioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarFuncionario(Pessoa pessoa)
        {
            var funcionario = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Funcionario"] = funcionario;
            return ViewComponent("CadastroConfiguracaoPessoaFuncionarioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirFuncionario(Pessoa pessoa)
        {
            var funcionario = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(funcionario);
            return ViewComponent("CadastroConfiguracaoPessoaFuncionarioLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarSocio(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaSocioLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirSocio()
        {
            ViewData["Socio"] = new Pessoa().SetarTransacionador(Transacionador.Socio);
            return ViewComponent("CadastroConfiguracaoPessoaSocioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarSocio(Pessoa pessoa)
        {
            var socio = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Socio"] = socio;
            return ViewComponent("CadastroConfiguracaoPessoaSocioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirSocio(Pessoa pessoa)
        {
            var socio = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(socio);
            return ViewComponent("CadastroConfiguracaoPessoaSocioLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarGoverno(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaGovernoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirGoverno()
        {
            ViewData["Governo"] = new Pessoa().SetarTransacionador(Transacionador.Governo);
            return ViewComponent("CadastroConfiguracaoPessoaGovernoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarGoverno(Pessoa pessoa)
        {
            var governo = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Governo"] = governo;
            return ViewComponent("CadastroConfiguracaoPessoaGovernoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirGoverno(Pessoa pessoa)
        {
            var governo = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(governo);
            return ViewComponent("CadastroConfiguracaoPessoaGovernoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarFinanciador(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaFinanciadorLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirFinanciador()
        {
            ViewData["Financiador"] = new Pessoa().SetarTransacionador(Transacionador.Financiador);
            return ViewComponent("CadastroConfiguracaoPessoaFinanciadorFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarFinanciador(Pessoa pessoa)
        {
            var financiador = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Financiador"] = financiador;
            return ViewComponent("CadastroConfiguracaoPessoaFinanciadorFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirFinanciador(Pessoa pessoa)
        {
            var financiador = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(financiador);
            return ViewComponent("CadastroConfiguracaoPessoaFinanciadorLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarBanco(Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();
            _pessoaService.Salvar(pessoa);
            return ViewComponent("CadastroConfiguracaoPessoaBancoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirBanco()
        {
            ViewData["Banco"] = new Pessoa().SetarTransacionador(Transacionador.Banco);
            return ViewComponent("CadastroConfiguracaoPessoaBancoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarBanco(Pessoa pessoa)
        {
            var banco = _pessoaService.ObterPorId(pessoa.Id).Result;
            ViewData["Banco"] = banco;
            return ViewComponent("CadastroConfiguracaoPessoaBancoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirBanco(Pessoa pessoa)
        {
            var banco = _pessoaService.ObterPorId(pessoa.Id).Result;
            _pessoaService.Excluir(banco);
            return ViewComponent("CadastroConfiguracaoPessoaBancoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarConta(Conta conta)
        {
            if (conta == null) return BadRequest();
            _contaService.Salvar(conta);
            return ViewComponent("CadastroConfiguracaoContaLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirConta()
        {
            ViewData["Conta"] = new Conta();
            return ViewComponent("CadastroConfiguracaoContaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarConta(Conta contaLista)
        {
            var conta = _contaService.ObterPorId(contaLista.Id).Result;
            ViewData["Conta"] = conta;
            return ViewComponent("CadastroConfiguracaoContaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirConta(int id)
        {
            var conta = _contaService.ObterPorId(id).Result;
            _contaService.Excluir(conta);
            return ViewComponent("CadastroConfiguracaoContaLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarCentroCusto(CentroCusto centroCusto)
        {
            if (centroCusto == null) return BadRequest();
            _centroCustoService.Salvar(centroCusto);
            return ViewComponent("CadastroConfiguracaoCentroCustoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirCentroCusto()
        {
            ViewData["CentroCusto"] = new CentroCusto();
            return ViewComponent("CadastroConfiguracaoCentroCustoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarCentroCusto(CentroCusto centroCusto)
        {
            ViewData["CentroCusto"] = centroCusto;
            return ViewComponent("CadastroConfiguracaoCentroCustoFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirCentroCusto(CentroCusto centroCusto)
        {
            _centroCustoService.Excluir(centroCusto);
            return ViewComponent("CadastroConfiguracaoCentroCustoLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarPlanoConta(PlanoConta planoConta)
        {
            if (planoConta == null) return BadRequest();
            _planoContaService.Salvar(planoConta);
            return ViewComponent("CadastroConfiguracaoPlanoContaLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirPlanoConta()
        {
            ViewData["PlanoConta"] = new PlanoConta();
            return ViewComponent("CadastroConfiguracaoPlanoContaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarPlanoConta(PlanoConta planoConta)
        {
            ViewData["PlanoConta"] = planoConta;
            return ViewComponent("CadastroConfiguracaoPlanoContaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult ExcluirPlanoConta(PlanoConta planoConta)
        {
            _planoContaService.Excluir(planoConta);
            return ViewComponent("CadastroConfiguracaoPlanoContaLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarEmpresa(Empresa empresa)
        {
            if (empresa == null) return BadRequest();
            _empresaService.Salvar(empresa);
            return ViewComponent("CadastroConfiguracaoEmpresaLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirEmpresa()
        {
            ViewData["Empresa"] = new Empresa();
            return ViewComponent("CadastroConfiguracaoEmpresaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarEmpresa(Empresa empresa)
        {
            ViewData["Empresa"] = empresa;
            return ViewComponent("CadastroConfiguracaoEmpresaFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(Usuario usuario)
        {
            if (usuario == null) return BadRequest();
            _usuarioService.Salvar(usuario);
            return ViewComponent("CadastroConfiguracaoUsuarioLista", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult InserirUsuario()
        {
            ViewData["Usuario"] = new Usuario();
            return ViewComponent("CadastroConfiguracaoUsuarioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult EditarUsuario(Usuario usuario)
        {
            ViewData["Usuario"] = usuario;
            return ViewComponent("CadastroConfiguracaoUsuarioFicha", new { maxPriority = 3, isDone = false });
        }

        [HttpPost]
        public ActionResult SalvarPessoaCadastroRapido(string nome, string transacionador)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(transacionador)) return BadRequest();
            var pessoa = new Pessoa { Nome = nome, Transacionador = transacionador };
            _pessoaService.Salvar(pessoa);
            return Ok(pessoa);
        }
    }
}