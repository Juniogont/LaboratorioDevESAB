using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Helpers;
using SGE.Models;
using SGE.Models.ViewModels;

namespace SGE.Controllers
{
    [Authorize]
    public class ContratosController : Controller
    {
        private readonly SGEMvcContext _context;

        public ContratosController(SGEMvcContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index()
        {
            var SGEMvcContext = _context.Contrato.Include(c => c.Entidade).Include(c => c.Evento).Include(c => c.ModeloContrato);
            return View(await SGEMvcContext.ToListAsync());
        }

        public IActionResult ContratoExiste(Contrato contrato)
        {           
            return View(contrato);
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Entidade)
                .Include(c => c.Evento)
                .Include(c => c.ModeloContrato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            if (contrato.Entidade == null)
                contrato.Entidade = await _context.Entidade.FindAsync(contrato.Evento.EntidadeId);

            var viewimprimir = new ViewModelContratoImprimir();

            var instrumentos = new List<string>();
            using (var _db = new SGEMvcContext())
            {
                var lstI = from o in _context.EventoItem
                               .Where(x => x.EventoId == contrato.EventoId).Distinct()
                           select o.NomeInstrumento;
                instrumentos = lstI.ToList();
            }
            var lstInstrum = new List<ViewModelEventoItens>();
            var soma = 0.00M;
            foreach (var item in instrumentos)
            {
                var obj = new ViewModelEventoItens();
                obj.Instrumento = item.ToUpper();
                obj.Itens = await _context.EventoItem.Where(x => x.EventoId == contrato.EventoId && x.NomeInstrumento == item).ToListAsync();
                var subTotStr = 0.0M;
                foreach (var it in obj.Itens)
                    subTotStr += it.ValorTotalItem;

                obj.Subtotal = subTotStr.ToString("C2");
                if (!lstInstrum.Select(x => x.Instrumento).Contains(item))
                {
                    soma += subTotStr;
                    lstInstrum.Add(obj);
                }
            }

           
            viewimprimir.Contrato = contrato;
            viewimprimir.Intrumentos = lstInstrum;

            if (soma > 0)
            {
                var ext = new ClsExtenso();
                var somaStr = ext.Extenso_Valor(soma);
                ViewData["ValorTotal"] = String.Format("<b>{0}({1})</b>", soma.ToString("C2"), somaStr);
            }
            else
                ViewData["ValorTotal"] = "<b>R$0,00</b>";

            ViewData["NumeroContrato"] = "CONTRATO Nº " + contrato.Numero + "/" + contrato.DataCadastro.Year.ToString();
            ViewData["Titulo"] = contrato.ModeloContrato.Titulo + " " + contrato.Entidade.Nome.ToUpper();
            var textopessoa = "pessoa física inscrita no CPF sob o número ";
            if (contrato.Entidade.TipoPessoa == TipoPessoa.Juridica)
                textopessoa = "pessoa jurídica inscrita no CNPJ sob o número ";
            ViewData["TextoInicial"] = contrato.ModeloContrato.TextoInicial + " " + contrato.Entidade.Nome.ToUpper() + " " + textopessoa + " " + contrato.Entidade.CPFCNPJ + " " + contrato.ModeloContrato.TextoInicialPosContratante;
            ViewData["Clausula1"] = contrato.ModeloContrato.ClausulaPrimeira + " " + contrato.Evento.dataInicio.ToLongDateString() + " às " + contrato.Evento.dataInicio.ToShortTimeString()
                + ", com término previsto para o dia " + contrato.Evento.dataFim.ToLongDateString() + " às " + contrato.Evento.dataFim.ToShortTimeString() + ". Relação de equipamentos transcritos abaixo:";

            ViewData["Clausula2"] = contrato.ModeloContrato.ClausulaSegunda + ": " + contrato.Evento.dataInicio.ToLongDateString() + " às " + contrato.Evento.dataInicio.ToShortTimeString()
              + ", com término previsto para o dia " + contrato.Evento.dataFim.ToLongDateString() + " às " + contrato.Evento.dataFim.ToShortTimeString() + ".";
            ViewData["Clausula3"] = contrato.ModeloContrato.ClausulaTerceira;
            ViewData["Clausula4"] = contrato.ModeloContrato.ClausulaQuarta;
            ViewData["Clausula5"] = contrato.ModeloContrato.ClausulaQuinta;
            ViewData["ClausulasFinais"] = contrato.ModeloContrato.ClausulasFinais;
            ViewData["LocalData"] = "Curitiba, " + DateTime.Now.ToLongDateString();

            return View(viewimprimir);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            var ultimNum = _context.Contrato.Max(x => x.Numero);
            if (!string.IsNullOrEmpty(ultimNum))
                ViewData["Numero"] = (Convert.ToInt32(ultimNum) + 1).ToString().PadLeft(3, '0');
            else
                ViewData["Numero"] = "001";
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "CPFCNPJ");
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento");
            ViewData["ModeloContratoId"] = new SelectList(_context.ModeloContrato, "Id", "Nome");
            return View();
        }

        // GET: Contratos/Create
        public async Task<IActionResult> Gerar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            evento.Entidade = await _context.Entidade.FindAsync(evento.EntidadeId);
            if (evento.Entidade == null)
            {
                return RedirectToAction("Edit", "Eventos", new { id, erro = "Cliente não informado para o evento." });
            }
            var ultimNum = _context.Contrato.Max(x => x.Numero);
            if (string.IsNullOrEmpty(evento.NumeroOS))
                ViewData["Numero"] = evento.NumeroProposta.PadLeft(4, '0');
            else
                ViewData["Numero"] = evento.NumeroOS.PadLeft(4, '0');
 
            ViewData["EventoId"] = evento.Id;
            ViewData["Cliente"] = evento.Entidade.Nome;
            ViewData["Evento"] = evento.NomeEvento;

            ViewData["ModeloContratoId"] = new SelectList(_context.ModeloContrato, "Id", "Nome");
            return View();
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeloContratoId,EventoId,EntidadeId,NumeroOS,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                var evento = await _context.Evento.FindAsync(contrato.EventoId);
                if (evento == null)
                {
                    return NotFound();
                }
                var existe = _context.Contrato.Include(c => c.Entidade)
                                            .Include(c => c.Evento)
                                            .Include(c => c.ModeloContrato)
                                            .FirstOrDefault(x => x.EventoId == contrato.EventoId); 

                if (existe != null)
                    return RedirectToAction(nameof(ContratoExiste), existe);       
                
                if (string.IsNullOrEmpty(evento.NumeroOS))
                    contrato.Numero = evento.NumeroProposta.PadLeft(4, '0');
                else
                    contrato.Numero = evento.NumeroOS.PadLeft(4, '0');

                contrato.DataCadastro = DateTime.Now;
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var ultimNum = _context.Contrato.Max(x => x.Numero);
            if (!string.IsNullOrEmpty(ultimNum))
                ViewData["Numero"] = (Convert.ToInt32(ultimNum) + 1).ToString().PadLeft(3, '0');
            else
                ViewData["Numero"] = "001";
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "CPFCNPJ", contrato.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", contrato.EventoId);
            ViewData["ModeloContratoId"] = new SelectList(_context.ModeloContrato, "Id", "Nome", contrato.ModeloContratoId);
            return View(contrato);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome", contrato.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", contrato.EventoId);
            ViewData["ModeloContratoId"] = new SelectList(_context.ModeloContrato, "Id", "Nome", contrato.ModeloContratoId);
            return View(contrato);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModeloContratoId,EventoId,EntidadeId,Numero,Id,Status")] Contrato contrato)
        {
            if (id != contrato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contrato.DataAlteracao = DateTime.Now;
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.Id))
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
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "Nome", contrato.EntidadeId);
            ViewData["EventoId"] = new SelectList(_context.Evento, "Id", "NomeEvento", contrato.EventoId);
            ViewData["ModeloContratoId"] = new SelectList(_context.ModeloContrato, "Id", "Nome", contrato.ModeloContratoId);
            return View(contrato);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Entidade)
                .Include(c => c.Evento)
                .Include(c => c.ModeloContrato)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contrato = await _context.Contrato.FindAsync(id);
            _context.Contrato.Remove(contrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
            return _context.Contrato.Any(e => e.Id == id);
        }
    }
}
