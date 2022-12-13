using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;
using SGE.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SGE.Controllers
{
    [Authorize]
    public class EntidadesController : Controller
    {
        private readonly SGEMvcContext _context;
        const int itensPorPagina = 10;
        public EntidadesController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Entidades
        public async Task<IActionResult> Index(int? pagina)
        {
            int numeroPagina = (pagina ?? 1);

            var resultado = new ViewModelEntidade();
            resultado.Entidades = await _context.Entidade.OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            foreach (var item in resultado.Entidades)
            {
                if (!string.IsNullOrEmpty(item.Nome))
                {
                    if (item.Nome.Length > 30)
                        item.Nome = item.Nome.Substring(0, 30);
                }

            }
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelEntidade v, int? pagina)
        {
            int numeroPagina = (pagina ?? 1);
            var resultado = new ViewModelEntidade();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Entidades = await _context.Entidade.Where(x => x.Nome.Contains(v.buscaNome) || x.NomeFantasia.Contains(v.buscaNome) || x.CPFCNPJ.Contains(v.buscaNome)).OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            else
                resultado.Entidades = await _context.Entidade.OrderBy(x => x.Nome).ToPagedListAsync(numeroPagina, itensPorPagina);
            if (v.TipoEntidade != null)
            {
                if (v.TipoEntidade == TipoEntidade.Cliente)
                    resultado.Entidades = resultado.Entidades.Where(x => x.Cliente == true).ToPagedList(numeroPagina, itensPorPagina);
                else
                    resultado.Entidades = resultado.Entidades.Where(x => x.Fornecedor == true).ToPagedList(numeroPagina, itensPorPagina);
            }
            foreach (var item in resultado.Entidades)
            {
                if (!string.IsNullOrEmpty(item.Nome))
                {
                    if (item.Nome.Length > 30)
                        item.Nome = item.Nome.Substring(0, 30);
                }

            }
            return View("Index", resultado);
        }

        // GET: Entidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidade = await _context.Entidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidade == null)
            {
                return NotFound();
            }

            return View(entidade);
        }

      
        // GET: Entidades/Create
        public IActionResult Create(string controle, string acao, int idorigem)
        {
            if (string.IsNullOrEmpty(controle))
                controle = "Entidades";
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        // POST: Entidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,NomeFantasia,TipoPessoa,CPFCNPJ,RGInscricaoEstadual,CEP,Logradouro,Numero,Complemento,Bairro,Telefone1,Telefone2,Celular,Email,WebSite,EstadoId,CidadeId,Observacoes,Cliente,Fornecedor,Id")] Entidade entidade, string controle, string acao, int idorigem)
        {
            if (ModelState.IsValid)
            {
                entidade.Status = Status.Ativo;
                entidade.DataCadastro = DateTime.Now;
                _context.Add(entidade);
                await _context.SaveChangesAsync();
                if (controle != "Entidades")
                    return RedirectToAction(acao, controle, new { id = idorigem });
                else
                    return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(controle))
                controle = "Entidades";
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            ViewBag.Estados = _context.Estado.ToList();
            return View(entidade);
        }


        // GET: Entidades/Create
        public IActionResult CreateFast(string controle, string acao, int idorigem)
        {
            ViewData["controle"] = controle;
            ViewData["acao"] = acao;
            ViewData["idorigem"] = idorigem;
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        // POST: Entidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFast([Bind("Nome,NomeFantasia,TipoPessoa,CPFCNPJ,RGInscricaoEstadual,CEP,Logradouro,Numero,Complemento,Bairro,Telefone1,Telefone2,Celular,Email,WebSite,Observacoes,Cliente,Fornecedor,Id")] Entidade entidade, string controle, string acao, int idorigem)
        {
            if (ModelState.IsValid)
            {
                entidade.Status = Status.Ativo;
                entidade.DataCadastro = DateTime.Now;
                _context.Add(entidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(acao, controle, new { id = idorigem });
            }
            ViewBag.Estados = _context.Estado.ToList();
            return View(entidade);
        }


        // GET: Entidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidade = await _context.Entidade.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }
            ViewBag.Estados = _context.Estado.ToList();
            if(entidade.EstadoId != null && entidade.EstadoId > 0)
            {
                ViewData["CidadeId"] = new SelectList(_context.Cidade.Where(x => x.IdEstado == entidade.EstadoId), "Id", "Nome", entidade.CidadeId);
            }
            ViewBag.Nome = entidade.Nome;
            return View(entidade);
        }

        // POST: Entidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,NomeFantasia,TipoPessoa,CPFCNPJ,RGInscricaoEstadual,CEP,Logradouro,Numero,Complemento,Bairro,Telefone1,Telefone2,Celular,Email,WebSite,EstadoId,CidadeId,Observacoes,Cliente,Fornecedor,Id,Status")] Entidade entidade)
        {
            if (id != entidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    entidade.DataAlteracao = DateTime.Now;
                    _context.Update(entidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntidadeExists(entidade.Id))
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
            ViewBag.Nome = entidade.Nome;
            ViewBag.Estados = _context.Estado.ToList();
            ViewBag.NomeEntidade = entidade.Nome;
            return View(entidade);
        }

        // GET: Entidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidade = await _context.Entidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidade == null)
            {
                return NotFound();
            }

            return View(entidade);
        }

        // POST: Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entidade = await _context.Entidade.FindAsync(id);
            _context.Entidade.Remove(entidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntidadeExists(int id)
        {
            return _context.Entidade.Any(e => e.Id == id);
        }
    }
}
