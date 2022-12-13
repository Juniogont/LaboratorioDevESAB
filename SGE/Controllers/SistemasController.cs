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

namespace SGE.Controllers
{
    [Authorize]
    public class SistemasController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly InstrumentoService _instrumentoService;

        public SistemasController(SGEMvcContext context, InstrumentoService instrumentoService)
        {
            _context = context;
            _instrumentoService = instrumentoService;
        }

        // GET: Sistemas
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.Sistema.Include(s => s.Fornecedor).Include(s => s.Instrumento).OrderBy(x => x.Nome);          
            var resultado = new ViewModelSistema();
            resultado.Sistemas = await SGEMvcContext.ToListAsync();
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelSistema v)
        {
            var resultado = new ViewModelSistema();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Sistemas = await _context.Sistema.Where(x => x.Nome.Contains(v.buscaNome) || x.Codigo == v.buscaNome || x.Descricao.Contains(v.buscaNome)).OrderBy(x => x.Nome).ToListAsync();
            else
                resultado.Sistemas = await _context.Sistema.OrderBy(x => x.Nome).ToListAsync();
            return View("Index", resultado);
        }

        // GET: Sistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistema
                .Include(s => s.Fornecedor)
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // GET: Sistemas/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome), "Id", "Nome");
            ViewBag.Instrumentos = _instrumentoService.FindAll();
            return View();
        }

        // POST: Sistemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,FornecedorId,InstrumentoId,Codigo,Imagem,NumeroSerie,QuantidadeEstoque,ValorSistema,ValorLocacao,DataAquisição,DataBaixa,Id")] Sistema sistema)
        {
         
            if (ModelState.IsValid)
            {
                sistema.DataCadastro = DateTime.Now;
                sistema.Status = Status.Ativo;
                _context.Add(sistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome), "Id", "Nome", sistema.FornecedorId);
            ViewBag.Instrumentos = _instrumentoService.FindAll();
            return View(sistema);
        }

        // GET: Sistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                try
                {
                    ViewBag.classeDadosTab = "";
                    ViewBag.classeDetalhesTab = "active";
                    id = Convert.ToInt32(TempData["IdSistema"]);
                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                ViewBag.classeDadosTab = "active";
                ViewBag.classeDetalhesTab = "";
            }
            var sistema = await _context.Sistema.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }
            try
            {
                if (TempData["salvouCase"].ToString() == "SIM")
                    ViewBag.msg = "Case incluido com sucesso";
                if (TempData["deletouCase"].ToString() == "SIM")
                    ViewBag.msg = "Case excluido com sucesso";
            }
            catch { }
         
            ViewData["FornecedorId"] = new SelectList(_context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome), "Id", "Nome", sistema.FornecedorId);
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento.OrderBy(x => x.Nome), "Id", "Nome", sistema.InstrumentoId);
            @ViewBag.Cases = await _context.Case.ToListAsync();
            ViewBag.SistemaId = id;
            ViewBag.Nome = sistema.Nome;
            sistema.Cases = _context.SistemaCases.Where(x => x.SistemaId == id).Include(s => s.Case).ToList();
            return View(sistema);
        }

        // POST: Sistemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Descricao,FornecedorId,InstrumentoId,Codigo,Imagem,NumeroSerie,QuantidadeEstoque,ValorSistema,ValorLocacao,DataAquisição,DataBaixa,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Sistema sistema)
        {
            if (id != sistema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemaExists(sistema.Id))
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
            ViewBag.Nome = sistema.Nome;
            ViewData["FornecedorId"] = new SelectList(_context.Entidade.Where(x => x.Fornecedor == true).OrderBy(x => x.Nome), "Id", "Nome", sistema.FornecedorId);
            ViewData["InstrumentoId"] = new SelectList(_context.Instrumento.OrderBy(x => x.Nome), "Id", "Nome", sistema.InstrumentoId);
            @ViewBag.Cases = await _context.Case.ToListAsync();
            return View(sistema);
        }

        // GET: Sistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistema
                .Include(s => s.Fornecedor)
                .Include(s => s.Instrumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // POST: Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sistema = await _context.Sistema.FindAsync(id);
            _context.Sistema.Remove(sistema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemaExists(int id)
        {
            return _context.Sistema.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarCase(int caseId, int sistemaId)
        {

            var sisCase = new SistemaCases();
            sisCase.CaseId = caseId;
            sisCase.SistemaId = sistemaId;
            _context.SistemaCases.Add(sisCase);
            await _context.SaveChangesAsync();
            ViewBag.Msg = "Case incluido com sucesso";
            //TempData["IdSistema"] = SistemaId;
            //TempData["salvouCase"] = "SIM";
            //ViewBag.classeDadosTab = "";
            //ViewBag.classeDetalhesTab = "active";
            //TempData["msg"] = "Case incluido com sucesso";
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), SistemaId);
            return ViewComponent("SistemaCases", new { SistemaId = sistemaId });
        }

        public async Task<IActionResult> DeleteSistemaCase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icase = await _context.SistemaCases.FirstOrDefaultAsync(m => m.Id == id);
            if (icase == null)
            {
                return NotFound();
            }
            var cs = await _context.Case.FirstOrDefaultAsync(m => m.Id == icase.CaseId);
            if (cs == null)
            {
                return NotFound();
            }
            return View(cs);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("DeleteSistemaCase")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSistemaCaseConfirmed(int id)
        {
            var @sistCase = await _context.SistemaCases.FindAsync(id);
            var idSistCase = @sistCase.SistemaId;
            TempData["IdSistema"] = idSistCase;
            TempData["salvouCase"] = "NAO";
            TempData["deletouCase"] = "SIM";
            _context.SistemaCases.Remove(@sistCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), idSistCase);
        }
    }
}
