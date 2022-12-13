using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Authorize]
    public class PosicoesAlmoxarifadoController : Controller
    {
        private readonly SGEMvcContext _context;

        public PosicoesAlmoxarifadoController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: PosicoesAlmoxarifado
        public async Task<IActionResult> Index()
        {
            return View(await _context.PosicaoAlmoxarifado.ToListAsync());
        }

        // GET: PosicoesAlmoxarifado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicaoAlmoxarifado = await _context.PosicaoAlmoxarifado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posicaoAlmoxarifado == null)
            {
                return NotFound();
            }

            return View(posicaoAlmoxarifado);
        }

        // GET: PosicoesAlmoxarifado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PosicoesAlmoxarifado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Observacao,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] PosicaoAlmoxarifado posicaoAlmoxarifado)
        {
            if (ModelState.IsValid)
            {
                posicaoAlmoxarifado.DataCadastro = DateTime.Now;
                posicaoAlmoxarifado.Status = Status.Ativo;
                _context.Add(posicaoAlmoxarifado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posicaoAlmoxarifado);
        }

        // GET: PosicoesAlmoxarifado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicaoAlmoxarifado = await _context.PosicaoAlmoxarifado.FindAsync(id);
            if (posicaoAlmoxarifado == null)
            {
                return NotFound();
            }
            ViewBag.Nome = posicaoAlmoxarifado.Nome;
            return View(posicaoAlmoxarifado);
        }

        // POST: PosicoesAlmoxarifado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Observacao,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] PosicaoAlmoxarifado posicaoAlmoxarifado)
        {
            if (id != posicaoAlmoxarifado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posicaoAlmoxarifado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosicaoAlmoxarifadoExists(posicaoAlmoxarifado.Id))
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
            ViewBag.Nome = posicaoAlmoxarifado.Nome;
            return View(posicaoAlmoxarifado);
        }

        // GET: PosicoesAlmoxarifado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posicaoAlmoxarifado = await _context.PosicaoAlmoxarifado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posicaoAlmoxarifado == null)
            {
                return NotFound();
            }

            return View(posicaoAlmoxarifado);
        }

        // POST: PosicoesAlmoxarifado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posicaoAlmoxarifado = await _context.PosicaoAlmoxarifado.FindAsync(id);
            _context.PosicaoAlmoxarifado.Remove(posicaoAlmoxarifado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosicaoAlmoxarifadoExists(int id)
        {
            return _context.PosicaoAlmoxarifado.Any(e => e.Id == id);
        }
    }
}
