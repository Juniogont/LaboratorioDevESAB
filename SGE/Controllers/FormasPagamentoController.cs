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
    public class FormasPagamentoController : Controller
    {
        private readonly SGEMvcContext _context;

        public FormasPagamentoController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: FormasPagamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormaPagamento.ToListAsync());
        }

        // GET: FormasPagamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // GET: FormasPagamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormasPagamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,MeioPagamento,QuantidadeParcelas,Id,Status")] FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                formaPagamento.DataCadastro = DateTime.Now;
                formaPagamento.Status = Status.Ativo;
                _context.Add(formaPagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formaPagamento);
        }

        // GET: FormasPagamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }
            ViewBag.Nome = formaPagamento.Nome;
            return View(formaPagamento);
        }

        // POST: FormasPagamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,MeioPagamento,QuantidadeParcelas,Id,Status")] FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    formaPagamento.DataAlteracao = DateTime.Now;
                    _context.Update(formaPagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormaPagamentoExists(formaPagamento.Id))
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
            ViewBag.Nome = formaPagamento.Nome;
            return View(formaPagamento);
        }

        // GET: FormasPagamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // POST: FormasPagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formaPagamento = await _context.FormaPagamento.FindAsync(id);
            _context.FormaPagamento.Remove(formaPagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormaPagamentoExists(int id)
        {
            return _context.FormaPagamento.Any(e => e.Id == id);
        }
    }
}
