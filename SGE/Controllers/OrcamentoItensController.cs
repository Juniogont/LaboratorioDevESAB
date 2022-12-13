using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    public class OrcamentoItensController : Controller
    {
        private readonly SGEMvcContext _context;

        public OrcamentoItensController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: OrcamentoItens
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.OrcamentoItem.Include(o => o.Case).Include(o => o.Equipamento).Include(o => o.Instrumento).Include(o => o.Orcamento).Include(o => o.Sistema);
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: OrcamentoItens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamentoItem = await _context.OrcamentoItem
                .Include(o => o.Case)
                .Include(o => o.Equipamento)
                .Include(o => o.Instrumento)
                .Include(o => o.Orcamento)
                .Include(o => o.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamentoItem == null)
            {
                return NotFound();
            }

            return View(orcamentoItem);
        }

        // GET: OrcamentoItens/Create
        public IActionResult Create()
        {
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome");
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome");
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento, "Id", "Id");
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento");
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome");
            return View();
        }

        // POST: OrcamentoItens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrcamentoId,CaseId,EquipamentoId,SistemaId,InstrumentoId,NomeInstrumento,NomeSistema,Descricao,Quantidade,Valor,Diarias,ValorTotalItem,ValorSublocacao")] OrcamentoItem orcamentoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orcamentoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", orcamentoItem.CaseId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", orcamentoItem.EquipamentoId);
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento, "Id", "Id", orcamentoItem.InstrumentoId);
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento", orcamentoItem.OrcamentoId);
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", orcamentoItem.SistemaId);
            return View(orcamentoItem);
        }

        // GET: OrcamentoItens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamentoItem = await _context.OrcamentoItem.FindAsync(id);
            if (orcamentoItem == null)
            {
                return NotFound();
            }
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", orcamentoItem.CaseId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", orcamentoItem.EquipamentoId);
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento, "Id", "Id", orcamentoItem.InstrumentoId);
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento", orcamentoItem.OrcamentoId);
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", orcamentoItem.SistemaId);
            return View(orcamentoItem);
        }

        // POST: OrcamentoItens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrcamentoId,CaseId,EquipamentoId,SistemaId,InstrumentoId,NomeInstrumento,NomeSistema,Descricao,Quantidade,Valor,Diarias,ValorTotalItem,ValorSublocacao")] OrcamentoItem orcamentoItem)
        {
            if (id != orcamentoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orcamentoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoItemExists(orcamentoItem.Id))
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
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", orcamentoItem.CaseId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", orcamentoItem.EquipamentoId);
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento, "Id", "Id", orcamentoItem.InstrumentoId);
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento", orcamentoItem.OrcamentoId);
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", orcamentoItem.SistemaId);
            return View(orcamentoItem);
        }

        // GET: OrcamentoItens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamentoItem = await _context.OrcamentoItem
                .Include(o => o.Case)
                .Include(o => o.Equipamento)
                .Include(o => o.Instrumento)
                .Include(o => o.Orcamento)
                .Include(o => o.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamentoItem == null)
            {
                return NotFound();
            }

            return View(orcamentoItem);
        }

        // POST: OrcamentoItens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orcamentoItem = await _context.OrcamentoItem.FindAsync(id);
            _context.OrcamentoItem.Remove(orcamentoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoItemExists(int id)
        {
            return _context.OrcamentoItem.Any(e => e.Id == id);
        }
    }
}
