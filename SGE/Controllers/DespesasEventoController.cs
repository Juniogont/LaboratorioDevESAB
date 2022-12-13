using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Helpers;
using SGE.Models;
using SGE.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Authorize]
    public class DespesasEventoController : Controller
    {
        private readonly SGEMvcContext _context;

        public DespesasEventoController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: DespesasEvento
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["MontagemId"] = new SelectList(_context.Set<Montagem>(), "Id", "Nome");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Nome");
            ViewData["EventoId"] = id;
            var model = new ViewModelDepesasEvento();
            model.DespesaEvento = new DespesaEvento();
            var SGEMvcContext = _context.DespesaEvento.Include(d => d.Montagem).Include(d => d.Funcionario).Where(x => x.EventoId == id);
            model.DespesasEvento = await SGEMvcContext.ToListAsync();
            return View(model);
        }

       
        // GET: DespesasEvento/Create
        public IActionResult Create()
        {
            ViewData["MontagemId"] = new SelectList(_context.Set<Montagem>(), "Id", "Nome");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Nome");
            return View();
        }

        // POST: DespesasEvento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaEvento.FuncionarioId,DespesaEvento.MontagemId,DespesaEvento.Almoco,DespesaEvento.Jantar,DespesaEvento.Lanche,DespesaEvento.Estadia,DespesaEvento.ValorDiversos,DespesaEvento.Id")] ViewModelDepesasEvento vmdespesaEvento, int? eventoId)
        {
            if (eventoId == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var dev = vmdespesaEvento.DespesaEvento;
                dev.EventoId = Convert.ToInt32(eventoId);
                _context.Add(dev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MontagemId"] = new SelectList(_context.Set<Montagem>(), "Id", "Nome");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Nome");
            ViewData["EventoId"] = eventoId;
            return View(vmdespesaEvento);
        }


        [HttpPost]
        public async Task<IActionResult> SalvarNovo(ViewModelDepesasEvento v, int? eventoId)
        {

            if (eventoId == null)
            {
                return NotFound();
            }
            var dv =  _context.DespesaViagem;
            var precos = await dv.ToListAsync();
            var evento = _context.Evento.Find(eventoId);
            if (dv == null)
            {
                return NotFound();
            }
            var u = new Utils();
            var qtdDiarias = u.QuantidadeDias(evento.dataInicio, evento.dataFim);


            var dev = v.DespesaEvento;
            dev.EventoId = Convert.ToInt32(eventoId);
            dev.ValorAlmoco = dev.Almoco ? precos[0].Almoco * qtdDiarias : 0.00M;
            dev.ValorEstadia = dev.Estadia ? precos[0].Estadia * qtdDiarias : 0.00M;
            dev.ValorJantar = dev.Jantar ? precos[0].Jantar * qtdDiarias : 0.00M;
            dev.ValorLanche = dev.Lanche ? precos[0].Lanche * qtdDiarias : 0.00M;
            dev.ValorDiversos = v.DespesaEvento.ValorDiversos;
            dev.ValorTotal = dev.ValorAlmoco + dev.ValorEstadia + dev.ValorJantar + dev.ValorLanche + dev.ValorDiversos;
            dev.FuncionarioId = v.DespesaEvento.FuncionarioId;
            dev.MontagemId = v.DespesaEvento.MontagemId;
            _context.Add(dev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = eventoId });
        }

       

        // GET: DespesasEvento/Delete/5
        public async Task<IActionResult> Delete(int? id, int? eventoId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesaEvento = await _context.DespesaEvento
                .Include(d => d.Montagem)
                .Include(d => d.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despesaEvento == null)
            {
                return NotFound();
            }
            ViewData["eventoId"] = eventoId;
            return View(despesaEvento);
        }

        // POST: DespesasEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? eventoId)
        {
            var despesaEvento = await _context.DespesaEvento.FindAsync(id);
            _context.DespesaEvento.Remove(despesaEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = eventoId });
        }

        private bool DespesaEventoExists(int id)
        {
            return _context.DespesaEvento.Any(e => e.Id == id);
        }
    }
}
