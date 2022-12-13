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
    public class LocaisEventosController : Controller
    {
        private readonly SGEMvcContext _context;

        public LocaisEventosController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: LocaisEventos
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.LocalEvento.Include(l => l.Cidade).Include(l => l.Estado).Include(l => l.Pais);
            foreach (var item in SGEMvcContext)
            {
                if (!string.IsNullOrEmpty(item.Nome))
                {
                    if (item.Nome.Length > 30)
                        item.Nome = item.Nome.Substring(0, 30);
                }

            }
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: LocaisEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvento = await _context.LocalEvento
                .Include(l => l.Cidade)
                .Include(l => l.Estado)
                .Include(l => l.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localEvento == null)
            {
                return NotFound();
            }

            return View(localEvento);
        }

        // GET: LocaisEventos/Create
        public IActionResult Create()
        {
            //ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome");
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["PaisId"] = new SelectList(_context.Pais, "Id", "Nome");
            return View();
        }

        // POST: LocaisEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Observacoes,Contato,PontoReferencia,Capacidade,CEP,Logradouro,Numero,Complemento,Bairro,CidadeId,EstadoId,PaisId,Telefone1,Telefone2,Celular,Email,WebSite,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] LocalEvento localEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", localEvento.CidadeId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["PaisId"] = new SelectList(_context.Pais, "Id", "Id", localEvento.PaisId);
            return View(localEvento);
        }

        // GET: LocaisEventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvento = await _context.LocalEvento.FindAsync(id);
            if (localEvento == null)
            {
                return NotFound();
            }
            ViewBag.Nome = localEvento.Nome;
            //ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", localEvento.CidadeId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["PaisId"] = new SelectList(_context.Pais, "Id", "Id", localEvento.PaisId);
            return View(localEvento);
        }

        // POST: LocaisEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Observacoes,Contato,PontoReferencia,Capacidade,CEP,Logradouro,Numero,Complemento,Bairro,CidadeId,EstadoId,PaisId,Telefone1,Telefone2,Celular,Email,WebSite,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] LocalEvento localEvento)
        {
            if (id != localEvento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalEventoExists(localEvento.Id))
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
            ViewBag.Nome = localEvento.Nome;
            //ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", localEvento.CidadeId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["PaisId"] = new SelectList(_context.Pais, "Id", "Id", localEvento.PaisId);
            return View(localEvento);
        }

        // GET: LocaisEventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvento = await _context.LocalEvento
                .Include(l => l.Cidade)
                .Include(l => l.Estado)
                .Include(l => l.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localEvento == null)
            {
                return NotFound();
            }

            return View(localEvento);
        }

        // POST: LocaisEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localEvento = await _context.LocalEvento.FindAsync(id);
            _context.LocalEvento.Remove(localEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalEventoExists(int id)
        {
            return _context.LocalEvento.Any(e => e.Id == id);
        }
    }
}
