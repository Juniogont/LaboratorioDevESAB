using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using System.Linq;

namespace SGE.Views.ViewComponents
{
    public class CaseEquipamentosViewComponent : ViewComponent
    {
        private readonly SGEMvcContext _context;
        public CaseEquipamentosViewComponent(SGEMvcContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int CaseId)
        {
            var result = _context.CaseEquipamentos.Where(x => x.CaseId == CaseId).Include(s => s.Equipamento).ToList();
            return View(result);
        }
    }
}
