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
    public class AlmoxarifadosController : Controller
    {
        private readonly SGEMvcContext _context;

        public AlmoxarifadosController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Almoxarifados
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.Almoxarifado.Include(a => a.LocalEvento).Include(a => a.PosicaoAlmoxarifado);
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: Almoxarifados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado
                .Include(a => a.LocalEvento)
                .Include(a => a.PosicaoAlmoxarifado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }

            return View(almoxarifado);
        }

        // GET: Almoxarifados/Create
        public IActionResult Create()
        {
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["PosicaoAlmoxarifadoId"] = _context.PosicaoAlmoxarifado.ToList();
            return View();
        }

        // POST: Almoxarifados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,QuantidadeEstoque,LocalEventoId,PosicaoAlmoxarifadoId,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Almoxarifado almoxarifado)
        {
            if (ModelState.IsValid)
            {
                almoxarifado.DataCadastro = DateTime.Now;
                almoxarifado.Status = Status.Ativo;
                _context.Add(almoxarifado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["PosicaoAlmoxarifadoId"] = _context.PosicaoAlmoxarifado.ToList();
            return View(almoxarifado);
        }

        // GET: Almoxarifados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado.FindAsync(id);
            if (almoxarifado == null)
            {
                return NotFound();
            }
            ViewBag.Nome = almoxarifado.Nome;
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["PosicaoAlmoxarifadoId"] = _context.PosicaoAlmoxarifado.ToList();
            return View(almoxarifado);
        }

        // POST: Almoxarifados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,QuantidadeEstoque,LocalEventoId,PosicaoAlmoxarifadoId,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Almoxarifado almoxarifado)
        {
            if (id != almoxarifado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    almoxarifado.DataAlteracao = DateTime.Now;
                    _context.Update(almoxarifado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlmoxarifadoExists(almoxarifado.Id))
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
            ViewBag.Nome = almoxarifado.Nome;
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["PosicaoAlmoxarifadoId"] = _context.PosicaoAlmoxarifado.ToList();
            return View(almoxarifado);
        }

        // GET: Almoxarifados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado
                .Include(a => a.LocalEvento)
                .Include(a => a.PosicaoAlmoxarifado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }

            return View(almoxarifado);
        }

        // POST: Almoxarifados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var almoxarifado = await _context.Almoxarifado.FindAsync(id);
            _context.Almoxarifado.Remove(almoxarifado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlmoxarifadoExists(int id)
        {
            return _context.Almoxarifado.Any(e => e.Id == id);
        }
    }
}
