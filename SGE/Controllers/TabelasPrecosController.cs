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
    public class TabelasPrecosController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly TabelaPrecoService _tabelaPrecoService;

        public TabelasPrecosController(SGEMvcContext context, TabelaPrecoService tabelaPrecoService)
        {
            _context = context;
            _tabelaPrecoService = tabelaPrecoService;
        }

        // GET: TabelasPrecos
        public IActionResult Index()
        {
            var viewTb = new ViewModelTabelaPreco();
            viewTb.Precos = _tabelaPrecoService.FindAllSistemas(1, null);
            ViewData["CheckedSistema"] = "checked";
            ViewData["CheckedCase"] = "";
            ViewData["CheckedEquipamento"] = "";
            ViewData["CheckedTabela1"] = "checked";
            ViewData["CheckedTabela2"] = "";
            ViewData["CheckedTabela3"] = "";
            ViewData["TipoPreco"] = 1;
            ViewData["Tabela"] = 1;

            return View(viewTb);
        }

        [HttpPost]
        public IActionResult Buscar(ViewModelTabelaPreco v)
        {
            var resultado = new ViewModelTabelaPreco();
            resultado.TipoPreco = v.TipoPreco;
            if (v.TipoPreco == 1)
            {
                resultado.Precos = _tabelaPrecoService.FindAllSistemas(v.Tabela, v.buscaNome);
                ViewData["CheckedSistema"] = "checked";
                ViewData["CheckedCase"] = "";
                ViewData["CheckedEquipamento"] = "";
                ViewData["tipo"] = "sist";
            }
            else if (v.TipoPreco == 2)
            {
                resultado.Precos = _tabelaPrecoService.FindAllCases(v.Tabela, v.buscaNome);
                ViewData["CheckedSistema"] = "";
                ViewData["CheckedCase"] = "checked";
                ViewData["CheckedEquipamento"] = "";
                ViewData["tipo"] = "case";
            }
            else if (v.TipoPreco == 3)
            {
                resultado.Precos = _tabelaPrecoService.FindAllEquipamentos(v.Tabela, v.buscaNome);
                ViewData["CheckedSistema"] = "";
                ViewData["CheckedCase"] = "";
                ViewData["CheckedEquipamento"] = "checked";
                ViewData["tipo"] = "equip";
            }
            ViewData["TipoPreco"] = v.TipoPreco;
            ViewData["Tabela"] = v.Tabela;

            if (v.Tabela == 1)
            {
                ViewData["CheckedTabela1"] = "checked";
                ViewData["CheckedTabela2"] = "";
                ViewData["CheckedTabela3"] = "";
            }
            else if (v.Tabela == 2)
            {
                ViewData["CheckedTabela1"] = "";
                ViewData["CheckedTabela2"] = "checked";
                ViewData["CheckedTabela3"] = "";
            }
            else if (v.Tabela == 3)
            {
                ViewData["CheckedTabela1"] = "";
                ViewData["CheckedTabela2"] = "";
                ViewData["CheckedTabela3"] = "checked";
            }

            return View("Index", resultado);
        }

        [HttpPost]
        public IActionResult AlterarPrecos(ViewModelTabelaPreco v)
        {
            

            var resultado = new ViewModelTabelaPreco();
            resultado.TipoPreco = v.TipoPreco;
            if (v.TipoPreco == 1)
            {
                resultado.Precos = _tabelaPrecoService.UpdateAllSistemas(v.Tabela, v.Percentual);
                ViewData["CheckedSistema"] = "checked";
                ViewData["CheckedCase"] = "";
                ViewData["CheckedEquipamento"] = "";

            }
            else if (v.TipoPreco == 2)
            {
                resultado.Precos = _tabelaPrecoService.UpdateAllCases(v.Tabela, v.Percentual);
                ViewData["CheckedSistema"] = "";
                ViewData["CheckedCase"] = "checked";
                ViewData["CheckedEquipamento"] = "";
            }
            else if (v.TipoPreco == 3)
            {
                resultado.Precos = _tabelaPrecoService.UpdateAllEquipamentos(v.Tabela, v.Percentual);
                ViewData["CheckedSistema"] = "";
                ViewData["CheckedCase"] = "";
                ViewData["CheckedEquipamento"] = "checked";
            }
            ViewData["TipoPreco"] = v.TipoPreco;
            ViewData["Tabela"] = v.Tabela;

            if (v.Tabela == 1)
            {
                ViewData["CheckedTabela1"] = "checked";
                ViewData["CheckedTabela2"] = "";
                ViewData["CheckedTabela3"] = "";
            }
            else if (v.Tabela == 2)
            {
                ViewData["CheckedTabela1"] = "";
                ViewData["CheckedTabela2"] = "checked";
                ViewData["CheckedTabela3"] = "";
            }
            else if (v.Tabela == 3)
            {
                ViewData["CheckedTabela1"] = "";
                ViewData["CheckedTabela2"] = "";
                ViewData["CheckedTabela3"] = "checked";
            }

            return View("Index", resultado);
        }

        

        // GET: TabelasPrecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco
                .Include(t => t.Case)
                .Include(t => t.Equipamento)
                .Include(t => t.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }

            return View(tabelaPreco);
        }

        // GET: TabelasPrecos/Create
        public IActionResult Create(string tipo, int id)
        {
            if (tipo == "sist")
            {
                ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", id);
                return View("CreateSist");
            }
            else if (tipo == "case")
            {
                ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", id);
                return View("CreateCase");
            }
            else
            {
                ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", id);
                return View("CreateEquip");
            }            
           
        }

        // POST: TabelasPrecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tabela,SistemaId,CaseId,EquipamentoId,ValorCompra,ValorLocacao,Id")] TabelaPreco tabelaPreco)
        {
            if (ModelState.IsValid)
            {
                tabelaPreco.DataCadastro = DateTime.Now;
                tabelaPreco.Status = Status.Ativo;
                _context.Add(tabelaPreco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", tabelaPreco.CaseId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", tabelaPreco.EquipamentoId);
            ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", tabelaPreco.SistemaId);
            return View(tabelaPreco);
        }

        // GET: TabelasPrecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco.FindAsync(id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }
            if(tabelaPreco.SistemaId != null)
            {
                ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", tabelaPreco.SistemaId);
                return View("EditSist", tabelaPreco);
            }
            else if (tabelaPreco.CaseId != null)
            {
                ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", tabelaPreco.CaseId);
                return View("EditCase", tabelaPreco);
            }
            else
            {
                ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", tabelaPreco.EquipamentoId);
                return View("EditEquip", tabelaPreco);
            }

        }

        // POST: TabelasPrecos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tabela,SistemaId,CaseId,EquipamentoId,ValorCompra,ValorLocacao,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] TabelaPreco tabelaPreco)
        {
            if (id != tabelaPreco.Id)
            {
                return NotFound();
            }
            var tp = await _context.TabelaPreco.FindAsync(id);
            if (ModelState.IsValid)
            {                
                try
                {
                    tp.ValorCompra = tabelaPreco.ValorCompra;
                    tp.ValorLocacao = tabelaPreco.ValorLocacao;
                    tp.DataAlteracao = DateTime.Now;
                    _context.Update(tp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabelaPrecoExists(tabelaPreco.Id))
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
            if (tp.SistemaId != null)
            {
                ViewData["SistemaId"] = new SelectList(_context.Sistema, "Id", "Nome", tabelaPreco.SistemaId);
                return View("EditSist", tabelaPreco);
            }
            else if (tp.CaseId != null)
            {
                ViewData["CaseId"] = new SelectList(_context.Case, "Id", "Nome", tabelaPreco.CaseId);
                return View("EditCase", tabelaPreco);
            }
            else
            {
                ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", tabelaPreco.EquipamentoId);
                return View("EditEquip", tabelaPreco);
            }
        }

        // GET: TabelasPrecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco
                .Include(t => t.Case)
                .Include(t => t.Equipamento)
                .Include(t => t.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }

            return View(tabelaPreco);
        }

        // POST: TabelasPrecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tabelaPreco = await _context.TabelaPreco.FindAsync(id);
            _context.TabelaPreco.Remove(tabelaPreco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabelaPrecoExists(int id)
        {
            return _context.TabelaPreco.Any(e => e.Id == id);
        }
    }
}
