using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Views.ViewComponents
{
    public class SistemaCasesViewComponent : ViewComponent
    {
        private readonly SGEMvcContext _context;
        public SistemaCasesViewComponent(SGEMvcContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int SistemaId)
        {
            var result = _context.SistemaCases.Where(x => x.SistemaId == SistemaId).Include(s => s.Case).ToList();
            return View(result);
        }
    }
}
