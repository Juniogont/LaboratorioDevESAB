using Microsoft.AspNetCore.Mvc;
using SGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Views.ViewComponents
{
    public class EventoFaturamentoViewComponent : ViewComponent
    {
        private readonly SGEMvcContext _context;
        public EventoFaturamentoViewComponent(SGEMvcContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int EventoId)
        {
            var result = _context.MovimentoFinanceiro.Where(x => x.EventoId == EventoId && x.TipoMovimentacao == Models.TipoMovimentacao.Entrada).ToList();
            return View(result);
        }
    }
}
