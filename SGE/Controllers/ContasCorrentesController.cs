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
    public class ContasCorrentesController : Controller
    {
        private readonly SGEMvcContext _context;

        public ContasCorrentesController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: ContasCorrentes
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.ContaCorrente.Include(c => c.Banco);
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: ContasCorrentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente
                .Include(c => c.Banco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        // GET: ContasCorrentes/Create
        public IActionResult Create()
        {
            ViewData["BancoId"] = new SelectList(_context.Set<Banco>(), "Id", "Nome");
            return View();
        }

        // POST: ContasCorrentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("numero,nome,agencia,BancoId,Id,")] ContaCorrente contaCorrente)
        {
            if (ModelState.IsValid)
            {
                contaCorrente.DataCadastro = DateTime.Now;
                contaCorrente.Status = Status.Ativo;
                _context.Add(contaCorrente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BancoId"] = new SelectList(_context.Set<Banco>(), "Id", "Nome", contaCorrente.BancoId);
            return View(contaCorrente);
        }

        // GET: ContasCorrentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente.FindAsync(id);
            if (contaCorrente == null)
            {
                return NotFound();
            }
            ViewBag.Nome = contaCorrente.nome;
            ViewData["BancoId"] = new SelectList(_context.Set<Banco>(), "Id", "Nome", contaCorrente.BancoId);
            return View(contaCorrente);
        }

        // POST: ContasCorrentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("numero,nome,agencia,BancoId,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] ContaCorrente contaCorrente)
        {
            if (id != contaCorrente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contaCorrente.DataAlteracao = DateTime.Now;
                    _context.Update(contaCorrente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaCorrenteExists(contaCorrente.Id))
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
            ViewData["BancoId"] = new SelectList(_context.Set<Banco>(), "Id", "Nome", contaCorrente.BancoId);
            return View(contaCorrente);
        }

        // GET: ContasCorrentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContaCorrente
                .Include(c => c.Banco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        // POST: ContasCorrentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaCorrente = await _context.ContaCorrente.FindAsync(id);
            _context.ContaCorrente.Remove(contaCorrente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaCorrenteExists(int id)
        {
            return _context.ContaCorrente.Any(e => e.Id == id);
        }
    }
}
