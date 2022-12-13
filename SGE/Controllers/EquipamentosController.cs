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
using SGE.Models.ViewModels;
using SGE.Services;
using X.PagedList;

namespace SGE.Controllers
{
    [Authorize]
    public class EquipamentosController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly InstrumentoService _instrumentoService;
        const int itensPorPagina = 10;
        public EquipamentosController(SGEMvcContext context, InstrumentoService instrumentoService)
        {
            _context = context;
            _instrumentoService = instrumentoService;
        }

        // GET: Equipamentos
        public async Task<IActionResult> Index(int? pagina)
        {
            int numeroPagina = (pagina ?? 1);
            var resultado = new ViewModelEquipamento();
            resultado.Equipamentos = await _context.Equipamento.OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelEquipamento v, int? pagina)
        {
            int numeroPagina = (pagina ?? 1);
            var resultado = new ViewModelEquipamento();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Equipamentos = await _context.Equipamento.Where(x => x.Nome.Contains(v.buscaNome) || x.Codigo == v.buscaNome || x.Descricao.Contains(v.buscaNome)).OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            else
                resultado.Equipamentos = await _context.Equipamento.OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            return View("Index", resultado);
        }

        // GET: Equipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // GET: Equipamentos/Create
        public IActionResult Create()
        {
            ViewBag.Instrumentos = _instrumentoService.FindAll();
            ViewBag.Fornecedores = _context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome).ToList();
            ViewBag.Cases = _context.Case.OrderBy(x => x.Nome).ToList();

            var viewModel = new Equipamento();
            return View();
        }

        // POST: Equipamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Codigo,Imagem,InstrumentoId,FornecedorId,NumeroSerie,QuantidadeEstoque,ValorSistema,ValorLocacao,DataAquisição,DataBaixa,Id")] Equipamento equipamento)
        {
            if (equipamento.FornecedorId == 0)
                equipamento.FornecedorId = 253;
            if (ModelState.IsValid)
            {               
                equipamento.DataCadastro = DateTime.Now;
                equipamento.Status = Status.Ativo;
                _context.Add(equipamento);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instrumentos = _instrumentoService.FindAll();
            ViewBag.Fornecedores = _context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome).ToList();
            ViewBag.Cases = _context.Case.OrderBy(x => x.Nome).ToList();
            return View(equipamento);
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }
            ViewBag.Nome = equipamento.Nome;
            return View(equipamento);
        }

        // POST: Equipamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,Codigo,Imagem,NumeroSerie,QuantidadeEstoque,ValorSistema,ValorLocacao,DataAquisição,DataBaixa,Id,Status")] Equipamento equipamento)
        {
            if (id != equipamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    equipamento.DataAlteracao = DateTime.Now;
                    _context.Update(equipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.Id))
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
            ViewBag.Nome = equipamento.Nome;
            return View(equipamento);
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // POST: Equipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await _context.Equipamento.FindAsync(id);
            _context.Equipamento.Remove(equipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamento.Any(e => e.Id == id);
        }
    }
}
