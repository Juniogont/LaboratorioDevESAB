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
    public class FinanceiroSaidasController : Controller
    {
        private readonly SGEMvcContext _context;

        public FinanceiroSaidasController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: MovimentosFinanceiros
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.MovimentoFinanceiro.Where(x => x.TipoMovimentacao == TipoMovimentacao.Saida).Include(m => m.ContaCorrente).Include(m => m.Entidade).Include(m => m.Evento).Include(m => m.FormaPagamento).Include(m => m.PlanoContas);
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: MovimentosFinanceiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentoFinanceiro = await _context.MovimentoFinanceiro
                .Include(m => m.ContaCorrente)
                .Include(m => m.Entidade)
                .Include(m => m.Evento)
                .Include(m => m.FormaPagamento)
                .Include(m => m.PlanoContas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentoFinanceiro == null)
            {
                return NotFound();
            }

            return View(movimentoFinanceiro);
        }

        // GET: MovimentosFinanceiros/Create
        public IActionResult Create()
        {
            ViewData["ContaCorrenteId"] = new SelectList(_context.ContaCorrente, "Id", "Nome");
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome");
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento");
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormaPagamento, "Id", "Nome");
            ViewData["PlanoContasId"] = new SelectList(_context.PlanoContas, "Id", "Nome");
            return View();
        }

        // POST: MovimentosFinanceiros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormaPagamentoId,PlanoContasId,ContaCorrenteId,EventoId,EntidadeId,NumeroDocumento,Descricao,Observacoes,dataVencimento,dataPagamento,Pago,Valor,Id,")] MovimentoFinanceiro movimentoFinanceiro)
        {
            if (ModelState.IsValid)
            {
                movimentoFinanceiro.TipoMovimentacao = TipoMovimentacao.Entrada;
                movimentoFinanceiro.DataCadastro = DateTime.Now;
                movimentoFinanceiro.Status = Status.Ativo;
                _context.Add(movimentoFinanceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContaCorrenteId"] = new SelectList(_context.ContaCorrente, "Id", "nome", movimentoFinanceiro.ContaCorrenteId);
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome", movimentoFinanceiro.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", movimentoFinanceiro.EventoId);
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormaPagamento, "Id", "Nome", movimentoFinanceiro.FormaPagamentoId);
            ViewData["PlanoContasId"] = new SelectList(_context.PlanoContas, "Id", "Nome", movimentoFinanceiro.PlanoContasId);
            return View(movimentoFinanceiro);
        }

        // GET: MovimentosFinanceiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentoFinanceiro = await _context.MovimentoFinanceiro.FindAsync(id);
            if (movimentoFinanceiro == null)
            {
                return NotFound();
            }
            ViewData["ContaCorrenteId"] = new SelectList(_context.ContaCorrente, "Id", "nome", movimentoFinanceiro.ContaCorrenteId);
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome", movimentoFinanceiro.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", movimentoFinanceiro.EventoId);
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormaPagamento, "Id", "Nome", movimentoFinanceiro.FormaPagamentoId);
            ViewData["PlanoContasId"] = new SelectList(_context.PlanoContas, "Id", "Nome", movimentoFinanceiro.PlanoContasId);
            return View(movimentoFinanceiro);
        }

        // POST: MovimentosFinanceiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormaPagamentoId,PlanoContasId,ContaCorrenteId,EventoId,EntidadeId,NumeroDocumento,Descricao,Observacoes,dataVencimento,dataPagamento,Pago,Valor,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] MovimentoFinanceiro movimentoFinanceiro)
        {
            if (id != movimentoFinanceiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    movimentoFinanceiro.DataAlteracao = DateTime.Now;
                    _context.Update(movimentoFinanceiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentoFinanceiroExists(movimentoFinanceiro.Id))
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
            ViewData["ContaCorrenteId"] = new SelectList(_context.ContaCorrente, "Id", "nome", movimentoFinanceiro.ContaCorrenteId);
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome", movimentoFinanceiro.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", movimentoFinanceiro.EventoId);
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormaPagamento, "Id", "Nome", movimentoFinanceiro.FormaPagamentoId);
            ViewData["PlanoContasId"] = new SelectList(_context.PlanoContas, "Id", "Nome", movimentoFinanceiro.PlanoContasId);
            return View(movimentoFinanceiro);
        }

        // GET: MovimentosFinanceiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentoFinanceiro = await _context.MovimentoFinanceiro
                .Include(m => m.ContaCorrente)
                .Include(m => m.Entidade)
                .Include(m => m.Evento)
                .Include(m => m.FormaPagamento)
                .Include(m => m.PlanoContas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentoFinanceiro == null)
            {
                return NotFound();
            }

            return View(movimentoFinanceiro);
        }

        // POST: MovimentosFinanceiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentoFinanceiro = await _context.MovimentoFinanceiro.FindAsync(id);
            _context.MovimentoFinanceiro.Remove(movimentoFinanceiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentoFinanceiroExists(int id)
        {
            return _context.MovimentoFinanceiro.Any(e => e.Id == id);
        }
    }
}
