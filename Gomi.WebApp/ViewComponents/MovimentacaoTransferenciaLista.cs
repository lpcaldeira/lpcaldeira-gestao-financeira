using Gomi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.ViewComponents
{
    [ViewComponent(Name = "MovimentacaoTransferenciaLista")]
    public class MovimentacaoTransferenciaLista : ViewComponent
    {
        private readonly ITransferenciaService _transferenciaService;

        public MovimentacaoTransferenciaLista(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var resultado = _transferenciaService.ObterTransferencias();
            return View(resultado);
        }
    }
}