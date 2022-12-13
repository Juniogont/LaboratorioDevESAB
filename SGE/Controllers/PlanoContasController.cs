using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Authorize]
    public class PlanoContasController : Controller
    {
        private readonly SGEMvcContext _context;

        public PlanoContasController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: PlanoContas
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanoContas.ToListAsync());
        }

        // GET: PlanoContas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoContas = await _context.PlanoContas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoContas == null)
            {
                return NotFound();
            }

            return View(planoContas);
        }

        // GET: PlanoContas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanoContas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoMovimentacao,Nome,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] PlanoContas planoContas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planoContas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planoContas);
        }

        // GET: PlanoContas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoContas = await _context.PlanoContas.FindAsync(id);
            if (planoContas == null)
            {
                return NotFound();
            }
            ViewBag.Nome = planoContas.Nome;
            return View(planoContas);
        }

        // POST: PlanoContas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoMovimentacao,Nome,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] PlanoContas planoContas)
        {
            if (id != planoContas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planoContas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoContasExists(planoContas.Id))
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
            ViewBag.Nome = planoContas.Nome;
            return View(planoContas);
        }

        // GET: PlanoContas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoContas = await _context.PlanoContas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoContas == null)
            {
                return NotFound();
            }

            return View(planoContas);
        }

        // POST: PlanoContas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planoContas = await _context.PlanoContas.FindAsync(id);
            _context.PlanoContas.Remove(planoContas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoContasExists(int id)
        {
            return _context.PlanoContas.Any(e => e.Id == id);
        }
    }
}
