using Microsoft.AspNetCore.Mvc;
using SGE.Data;
using System.Linq;

namespace SGE.Views.ViewComponents
{
    public class OrcamentoItensViewComponent : ViewComponent
    {
        private readonly SGEMvcContext _context;
        public OrcamentoItensViewComponent(SGEMvcContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int OrcamentoId)
        {
            var result = _context.OrcamentoItem.Where(x => x.OrcamentoId == OrcamentoId).ToList();
            var valor = 0.0M;
            foreach (var item in result)
            {
                valor = valor + item.ValorTotalItem;
            }
            var orcamento = _context.Orcamento.FirstOrDefault(x => x.Id == OrcamentoId);
            if(orcamento != null)
            {
                orcamento.ValorTotal = valor;
                _context.Update(orcamento);
                _context.SaveChanges();
            }
            ViewData["valorOrcamento"] = valor.ToString("N2");
            return View(result);
        }
    }
}
