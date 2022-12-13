using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    [Authorize]
    public class InstrumentosController : Controller
    {
        private readonly SGEMvcContext _context;

        public InstrumentosController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Instrumentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instrumento.OrderBy(x => x.Nome).ToListAsync());
        }

        // GET: Instrumentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumento = await _context.Instrumento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrumento == null)
            {
                return NotFound();
            }

            return View(instrumento);
        }

        // GET: Instrumentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instrumentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao")] Instrumento instrumento)
        {
            if (ModelState.IsValid)
            {
                instrumento.DataCadastro = DateTime.Now;
                instrumento.Status = Status.Ativo;
                _context.Add(instrumento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrumento);
        }

        // GET: Instrumentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumento = await _context.Instrumento.FindAsync(id);
            if (instrumento == null)
            {
                return NotFound();
            }
            ViewBag.Nome = instrumento.Nome;
            return View(instrumento);
        }

        // POST: Instrumentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Status")] Instrumento instrumento)
        {
            if (id != instrumento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    instrumento.DataAlteracao = DateTime.Now;
                    _context.Update(instrumento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrumentoExists(instrumento.Id))
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
            ViewBag.Nome = instrumento.Nome;
            return View(instrumento);
        }

        // GET: Instrumentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumento = await _context.Instrumento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrumento == null)
            {
                return NotFound();
            }

            return View(instrumento);
        }

        // POST: Instrumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instrumento = await _context.Instrumento.FindAsync(id);
            _context.Instrumento.Remove(instrumento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstrumentoExists(int id)
        {
            return _context.Instrumento.Any(e => e.Id == id);
        }
    }
}
