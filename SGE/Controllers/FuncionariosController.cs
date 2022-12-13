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
    public class FuncionariosController : Controller
    {
        private readonly SGEMvcContext _context;

        public FuncionariosController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.Funcionario.Include(f => f.Cidade).Include(f => f.CidadeNasc).Include(f => f.Estado).Include(f => f.EstadoNasc).Include(f => f.Pais);
            return View(await SGEMvcContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Cidade)
                .Include(f => f.CidadeNasc)
                .Include(f => f.Estado)
                .Include(f => f.EstadoNasc)
                .Include(f => f.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome");
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome");
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Tratamento,Cargo,EstadoCivil,Escolaridade,DataNascimento,NomePai,NomeMae,EstadoNascId,CidadeNascId,CEP,Logradouro,Numero,Complemento,Bairro,CidadeId,EstadoId,PaisId,Telefone1,Telefone2,Celular,Email,DataAdmissao,DataDemissao,CPF,RG,OrgaoEmissor,CTPS,CNH,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeId);
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeNascId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult CreateFast(string controle, string acao, int idorigem)
        {
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome");
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome");
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFast([Bind("Nome,Tratamento,Cargo,EstadoCivil,Escolaridade,DataNascimento,NomePai,NomeMae,EstadoNascId,CidadeNascId,CEP,Logradouro,Numero,Complemento,Bairro,CidadeId,EstadoId,PaisId,Telefone1,Telefone2,Celular,Email,DataAdmissao,DataDemissao,CPF,RG,OrgaoEmissor,CTPS,CNH,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Funcionario funcionario, string controle, string acao, int idorigem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                if (controle != "Funcionarios")
                    return RedirectToAction(acao, controle, new { id = idorigem });
                else
                    return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeId);
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeNascId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["NomeFuncionario"] = funcionario.Nome;
            ViewBag.Nome = funcionario.Nome;
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeId);
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeNascId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Tratamento,Cargo,EstadoCivil,Escolaridade,DataNascimento,NomePai,NomeMae,EstadoNascId,CidadeNascId,CEP,Logradouro,Numero,Complemento,Bairro,CidadeId,EstadoId,PaisId,Telefone1,Telefone2,Celular,Email,DataAdmissao,DataDemissao,CPF,RG,OrgaoEmissor,CTPS,CNH,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
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
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeId);
            ViewData["CidadeNascId"] = new SelectList(_context.Cidade, "Id", "Nome", funcionario.CidadeNascId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["EstadoNascId"] = _context.Estado.ToList();
            ViewBag.Nome = funcionario.Nome;
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Cidade)
                .Include(f => f.CidadeNasc)
                .Include(f => f.Estado)
                .Include(f => f.EstadoNasc)
                .Include(f => f.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.Id == id);
        }
    }
}
