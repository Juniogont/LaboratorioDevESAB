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
    public class ModelosContratoController : Controller
    {
        private readonly SGEMvcContext _context;

        public ModelosContratoController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: ModelosContrato
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModeloContrato.ToListAsync());
        }

        // GET: ModelosContrato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloContrato = await _context.ModeloContrato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloContrato == null)
            {
                return NotFound();
            }

            return View(modeloContrato);
        }

        // GET: ModelosContrato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelosContrato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Titulo,TextoInicial,TextoInicialPosContratante,ClausulaPrimeira,ClausulaSegunda,ClausulaTerceira,ClausulaQuarta,ClausulaQuinta,ClausulasFinais,Id")] ModeloContrato modeloContrato)
        {
            if (ModelState.IsValid)
            {
                modeloContrato.Status = Status.Ativo;
                modeloContrato.DataCadastro = DateTime.Now;
                _context.Add(modeloContrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modeloContrato);
        }

        // GET: ModelosContrato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloContrato = await _context.ModeloContrato.FindAsync(id);
            if (modeloContrato == null)
            {
                return NotFound();
            }
            ViewBag.Nome = modeloContrato.Nome;
            return View(modeloContrato);
        }

        // POST: ModelosContrato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Titulo,TextoInicial,TextoInicialPosContratante,ClausulaPrimeira,ClausulaSegunda,ClausulaTerceira,ClausulaQuarta,ClausulaQuinta,ClausulasFinais,Id,Status")] ModeloContrato modeloContrato)
        {
            if (id != modeloContrato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    modeloContrato.DataAlteracao = DateTime.Now;
                    _context.Update(modeloContrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloContratoExists(modeloContrato.Id))
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
            ViewBag.Nome = modeloContrato.Nome;
            return View(modeloContrato);
        }

        // GET: ModelosContrato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloContrato = await _context.ModeloContrato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloContrato == null)
            {
                return NotFound();
            }

            return View(modeloContrato);
        }

        // POST: ModelosContrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeloContrato = await _context.ModeloContrato.FindAsync(id);
            _context.ModeloContrato.Remove(modeloContrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloContratoExists(int id)
        {
            return _context.ModeloContrato.Any(e => e.Id == id);
        }
    }
}
