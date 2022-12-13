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
    public class DespesasViagemController : Controller
    {
        private readonly SGEMvcContext _context;

        public DespesasViagemController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: DespesasViagem
        public async Task<IActionResult> Index()
        {
            return View(await _context.DespesaViagem.ToListAsync());
        }

        // GET: DespesasViagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesaViagem = await _context.DespesaViagem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despesaViagem == null)
            {
                return NotFound();
            }

            return View(despesaViagem);
        }

        // GET: DespesasViagem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DespesasViagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Almoco,Jantar,Lanche,Estadia,CoeficienteKM,CoeficienteNF,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] DespesaViagem despesaViagem)
        {
            if (ModelState.IsValid)
            {
                despesaViagem.DataCadastro = DateTime.Now;
                _context.Add(despesaViagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(despesaViagem);
        }

        // GET: DespesasViagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesaViagem = await _context.DespesaViagem.FindAsync(id);
            if (despesaViagem == null)
            {
                return NotFound();
            }
            return View(despesaViagem);
        }

        // POST: DespesasViagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Almoco,Jantar,Lanche,Estadia,CoeficienteKM,CoeficienteNF,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] DespesaViagem despesaViagem)
        {
            if (id != despesaViagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    despesaViagem.DataAlteracao = DateTime.Now;
                    _context.Update(despesaViagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaViagemExists(despesaViagem.Id))
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
            return View(despesaViagem);
        }

        // GET: DespesasViagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesaViagem = await _context.DespesaViagem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despesaViagem == null)
            {
                return NotFound();
            }

            return View(despesaViagem);
        }

        // POST: DespesasViagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var despesaViagem = await _context.DespesaViagem.FindAsync(id);
            _context.DespesaViagem.Remove(despesaViagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaViagemExists(int id)
        {
            return _context.DespesaViagem.Any(e => e.Id == id);
        }
    }
}
