using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;
using SGE.Models.ViewModels;
using SGE.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Authorize]
    public class CasesController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly EquipamentoService _equipamentoService;

        public CasesController(SGEMvcContext context, EquipamentoService equipamentoService)
        {
            _context = context;
            _equipamentoService = equipamentoService;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var resultado = new ViewModelCase();
            resultado.Cases = await _context.Case.OrderBy(x => x.Nome).ToListAsync();
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelCase v)
        {
            var resultado = new ViewModelCase();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Cases = await _context.Case.Where(x => x.Nome.Contains(v.buscaNome) || x.Codigo == v.buscaNome || x.Descricao.Contains(v.buscaNome)).OrderBy(x => x.Nome).ToListAsync();
            else
                resultado.Cases = await _context.Case.OrderBy(x => x.Nome).ToListAsync();
            return View("Index", resultado);
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var icase = await _context.Case.FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }

            return View(icase);
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome");
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Codigo,SistemaId,Id")] Case @case)
        {
            if (ModelState.IsValid)
            {
                @case.DataCadastro = DateTime.Now;
                @case.Status = Status.Ativo;
                _context.Add(@case);
                await _context.SaveChangesAsync();
                var idcase = @case.Id;
                TempData["IdCase"] = idcase;
                return RedirectToAction(nameof(Edit), idcase);
            }
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", @case.SistemaId);
            return View(@case);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarEquipamento(int EquipId, int Caseid)
        {

            var equiCase = new CaseEquipamentos();
            equiCase.CaseId = Caseid;
            equiCase.EquipamentoId = EquipId;
            _context.CaseEquipamentos.Add(equiCase);
            await _context.SaveChangesAsync();
            TempData["IdCase"] = Caseid;
            TempData["salvouEquip"] = "SIM";
            ViewBag.classeDadosTab = "";
            ViewBag.classeDetalhesTab = "active";
            ViewBag.Msg = "Equipamento incluido com sucesso";
            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("CaseEquipamentos", new { CaseId = Caseid });
        }
  
        public async Task<IActionResult> CarregarPartial(int Caseid)
        {
           
            var model = await _context.Case.FindAsync(Caseid);
            model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            return PartialView("ViewCaseEquipamentos", model);
        }

        public PartialViewResult ViewCaseEquipamentos()
        {
            return PartialView();
        }

        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                try
                {
                    ViewBag.classeDadosTab = "";
                    ViewBag.classeDetalhesTab = "active";
                    id = Convert.ToInt32(TempData["IdCase"]);
                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                ViewBag.classeDadosTab = "active";
                ViewBag.classeDetalhesTab = "";
            }
            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {

                return NotFound();
            }
            try
            {
                if (TempData["salvouEquip"].ToString() == "SIM")
                    ViewBag.msg = "Equipamento incluido com sucesso";
                else if (TempData["deletouEquip"].ToString() == "SIM")
                    ViewBag.msg = "Equipamento excluído com sucesso";
            }
            catch {}
            
            @case.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == id).Include(s => s.Equipamento).ToList();
            ViewBag.Equipamentos = _equipamentoService.FindAllNomeCodigo();
            ViewBag.CaseID = id;
            ViewBag.Nome = @case.Nome;
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", @case.SistemaId);
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Codigo,SistemaId,Id,Status")] Case @case)
        {
            if (id != @case.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    @case.DataAlteracao = DateTime.Now;
                    _context.Update(@case);
                    var sistemaCase = _context.SistemaCases.FirstOrDefault(x => x.CaseId == @case.Id && x.SistemaId == @case.SistemaId);
                    if(sistemaCase == null)
                    {
                        sistemaCase = new SistemaCases();
                        sistemaCase.CaseId = @case.Id;
                        sistemaCase.SistemaId = @case.SistemaId;
                        _context.SistemaCases.Add(sistemaCase);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Nome = @case.Nome;
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", @case.SistemaId);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icase = await _context.Case.FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }

            return View(icase);
        }

        public async Task<IActionResult> DeleteCaseEquipamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icase = await _context.CaseEquipamentos.FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }
            var equip = await _context.Equipamento.FirstOrDefaultAsync(m => m.Id == icase.EquipamentoId);
            if (equip == null)
            {
                return NotFound();
            }
            return View(equip);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @case = await _context.Case.FindAsync(id);
            _context.Case.Remove(@case);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("DeleteCaseEquipamento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCaseEquipamentoConfirmed(int id)
        {
            var @case = await _context.CaseEquipamentos.FindAsync(id);
            var idcase = @case.CaseId;
            TempData["IdCase"] = idcase;
            TempData["salvouEquip"] = "NAO";
            TempData["deletouEquip"] = "SIM";
            _context.CaseEquipamentos.Remove(@case);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), idcase);
        }

        private bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.Id == id);
        }
    }
}
