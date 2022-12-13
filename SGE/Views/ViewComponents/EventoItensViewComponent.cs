using Microsoft.AspNetCore.Mvc;
using SGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Views.ViewComponents
{
    public class EventoItensViewComponent : ViewComponent
    {
        private readonly SGEMvcContext _context;
        public EventoItensViewComponent(SGEMvcContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int EventoId)
        {
            var result = _context.EventoItem.Where(x => x.EventoId == EventoId).ToList();
            var valor = 0.0M;
            foreach (var item in result)
            {
                valor = valor + item.ValorTotalItem;
            }
            var evento = _context.Evento.FirstOrDefault(x => x.Id == EventoId);
            if (evento != null)
            {
                evento.ValorTotal = valor;
                _context.Update(evento);
                _context.SaveChanges();
            }
            ViewData["valorOrcamento"] = valor.ToString("N2");
            return View(result);
        }
    }
}
