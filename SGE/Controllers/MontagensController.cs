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
    public class MontagensController : Controller
    {
        private readonly SGEMvcContext _context;

        public MontagensController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Montagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Montagem.ToListAsync());
        }

        // GET: Montagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var montagem = await _context.Montagem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (montagem == null)
            {
                return NotFound();
            }

            return View(montagem);
        }

        // GET: Montagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Montagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Montagem montagem)
        {
            if (ModelState.IsValid)
            {
                montagem.DataCadastro = DateTime.Now;
                montagem.Status = Status.Ativo;
                _context.Add(montagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(montagem);
        }


        // GET: Montagems/Create
        public IActionResult CreateFast(string controle, string acao, int idorigem)
        {
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            return View();
        }

        // POST: Montagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFast([Bind("Nome,Id")] Montagem montagem, string controle, string acao, int idorigem)
        {
            if (ModelState.IsValid)
            {
                montagem.DataCadastro = DateTime.Now;
                montagem.Status = Status.Ativo;
                _context.Add(montagem);
                await _context.SaveChangesAsync();
                if (controle != "Montagens")
                    return RedirectToAction(acao, controle, new { id = idorigem });
                else
                    return RedirectToAction(nameof(Index));
            }
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            return View(montagem);
        }

        // GET: Montagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var montagem = await _context.Montagem.FindAsync(id);
            if (montagem == null)
            {
                return NotFound();
            }
            ViewBag.Nome = montagem.Nome;
            return View(montagem);
        }

        // POST: Montagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id,Status")] Montagem montagem)
        {
            if (id != montagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    montagem.DataAlteracao = DateTime.Now;
                    _context.Update(montagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MontagemExists(montagem.Id))
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
            ViewBag.Nome = montagem.Nome;
            return View(montagem);
        }

        // GET: Montagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var montagem = await _context.Montagem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (montagem == null)
            {
                return NotFound();
            }

            return View(montagem);
        }

        // POST: Montagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var montagem = await _context.Montagem.FindAsync(id);
            _context.Montagem.Remove(montagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MontagemExists(int id)
        {
            return _context.Montagem.Any(e => e.Id == id);
        }
    }
}
