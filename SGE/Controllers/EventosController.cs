using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Helpers;
using SGE.Models;
using SGE.Models.ViewModels;
using SGE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly EquipamentoService _equipamentoService;
        private readonly InstrumentoService _instrumentoService;

        public EventosController(SGEMvcContext context, EquipamentoService equipamentoService, InstrumentoService instrumentoService)
        {
            _context = context;
            _equipamentoService = equipamentoService;
            _instrumentoService = instrumentoService;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var resultado = new ViewModelEvento();
            resultado.Eventos = await _context.Evento.Include(e => e.Cidade).Include(e => e.Estado).ToListAsync();

            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelEvento v)
        {
            var resultado = new ViewModelEvento();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Eventos = await _context.Evento.Where(x => x.NomeSolicitante.Contains(v.buscaNome) || x.NumeroOS.Contains(v.buscaNome) || x.Cidade.Nome.Contains(v.buscaNome) || x.Estado.Sigla.Contains(v.buscaNome)).Include(e => e.Cidade).Include(e => e.Estado).OrderByDescending(x => x.dataInicio).ToListAsync();
            else
                resultado.Eventos = await _context.Evento.OrderByDescending(x => x.dataInicio).Include(e => e.Cidade).Include(e => e.Estado).ToListAsync();

            return View("Index", resultado);
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Cidade)
                .Include(e => e.Entidade)
                .Include(e => e.Estado)
                .Include(e => e.Funcionario)
                .Include(e => e.LocalEvento)
                .Include(e => e.Orcamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            var viewimprimir = new ViewModelEventoImprimir();

            var instrumentos = new List<string>();
            using (var _db = new SGEMvcContext())
            {
                var lstI = from o in _context.EventoItem
                               .Where(x => x.EventoId == id).Distinct()
                           select o.NomeInstrumento;
                instrumentos = lstI.ToList();
            }
            var lstInstrum = new List<ViewModelEventoItens>();

            foreach (var item in instrumentos)
            {
                var obj = new ViewModelEventoItens();
                obj.Instrumento = item.ToUpper();
                obj.Itens = await _context.EventoItem.Where(x => x.EventoId == id && x.NomeInstrumento == item).ToListAsync();
                if (!lstInstrum.Select(x => x.Instrumento).Contains(item))
                {
                    lstInstrum.Add(obj);
                }
            }

            var despesas = _context.DespesaEvento.Include(d => d.Montagem).Include(d => d.Funcionario).Where(x => x.EventoId == id).ToList();

            viewimprimir.Evento = evento;
            viewimprimir.Intrumentos = lstInstrum;
            viewimprimir.DespesasEvento = despesas;


            ViewData["NumeroContrato"] = "OS Nº " + evento.NumeroOS + "/" + evento.DataCadastro.Year.ToString();
            ViewData["DataInicio"] = evento.dataInicio.ToShortDateString();
            ViewData["HoraInicio"] = evento.dataInicio.ToShortTimeString();
            ViewData["DataFim"] = evento.dataFim.ToShortDateString();
            ViewData["HoraFim"] = evento.dataFim.ToShortTimeString();
            ViewData["LocalData"] = "Curitiba, " + DateTime.Now.ToLongDateString();

            return View(viewimprimir);
        }



        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Id");
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "CPFCNPJ");
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Id");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id");
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Id");
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrcamentoId,DataAprovacao,NumeroOS,NumeroProposta,NomeEvento,PontoReferencia,LocalEventoId,SituacaoEvento,dataProposta,dataInicio,dataFim,dataMontagem,dataDesmontagem,TipoProposta,EntidadeId,NomeSolicitante,EmpresaSolicitante,Telefone,Email,EstadoId,CidadeId,MostrarValoresUnitarios,MostrarValorTotal,ValorTotal,ValorDesconto,CondicoesPagamento,Entrega,ValidadeProposta,Observacoes,ObservacoesChecklist,FuncionarioId,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Id", evento.CidadeId);
            ViewData["EntidadeId"] = new SelectList(_context.Entidade, "Id", "CPFCNPJ", evento.EntidadeId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Id", evento.EstadoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", evento.FuncionarioId);
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Id", evento.LocalEventoId);
            ViewData["OrcamentoId"] = new SelectList(_context.Orcamento, "Id", "NomeEvento", evento.OrcamentoId);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id, string erro)
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
            if (string.IsNullOrEmpty(evento.NumeroOS))
                evento.NumeroOS = evento.NumeroProposta.PadLeft(4,'0');
            else
                evento.NumeroOS = evento.NumeroOS.PadLeft(4, '0');

            ViewData["Erro"] = erro;
            ViewData["TipoItem"] = "TipoSistema";
            ViewData["NomeBtnItens"] = "Incluir Sistema";
            ViewData["EventoId"] = id;
            ViewData["OrcamentoId"] = evento.OrcamentoId;
            ViewData["Equipamentos"] = _equipamentoService.FindSistemasNomeCodigo();
            ViewData["Instrumentos"] = _instrumentoService.FindAll();
            ViewData["FormaPagamentoId"] = _context.FormaPagamento.Where(x => x.Status == Status.Ativo).ToList();
            ViewData["EntidadeFaturamentoId"] = _context.Entidade.Where(x => x.Cliente == true).ToList();
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Sigla", evento.EstadoId);
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", evento.CidadeId);
            //ViewData["EntidadeId"] = new SelectList(_context.Entidade.Where(x => x.Cliente == true).ToList(), "Id", "Nome", evento.EntidadeId);
            ViewData["Entidades"] = _context.Entidade.Where(x => x.Cliente == true).ToList();
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Nome", evento.LocalEventoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Nome", evento.FuncionarioId);
            ViewData["Nome"] = evento.NomeEvento;
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrcamentoId,DataAprovacao,NumeroOS,NumeroProposta,NomeEvento,PontoReferencia,LocalEventoId,SituacaoEvento,dataProposta,dataInicio,dataFim,dataMontagem,dataDesmontagem,TipoProposta,EntidadeId,NomeSolicitante,EmpresaSolicitante,Telefone,Email,EstadoId,CidadeId,MostrarValoresUnitarios,MostrarValorTotal,ValorTotal,ValorDesconto,CondicoesPagamento,Entrega,ValidadeProposta,Observacoes,ObservacoesChecklist,FuncionarioId,Id,DataCadastro,UsuarioCadastro,DataAlteracao,UsuarioAlteracao,Status")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (evento.EntidadeId == null)
                        ViewData["Erro"] = "Cliente não informado";
                    else
                    {
                        _context.Update(evento);
                        await _context.SaveChangesAsync();
                    }
                   
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EventoExists(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewData["Erro"] = ex.Message;
                    }
                }
            }
            ViewData["EventoId"] = id;
            ViewData["OrcamentoId"] = evento.OrcamentoId;
            ViewData["Equipamentos"] = _equipamentoService.FindSistemasNomeCodigo();
            ViewData["Instrumentos"] = _instrumentoService.FindAll();
            ViewData["EntidadeId"] = _context.Entidade.Where(x => x.Cliente == true).ToList();
            ViewData["FormaPagamentoId"] = _context.FormaPagamento.Where(x => x.Status == Status.Ativo).ToList();
            ViewData["EntidadeFaturamentoId"] = _context.Entidade.Where(x => x.Cliente == true).ToList();
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Sigla", evento.EstadoId);
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", evento.CidadeId);
            ViewData["Entidades"] = _context.Entidade.Where(x => x.Cliente == true).ToList();
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Nome", evento.LocalEventoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Nome", evento.FuncionarioId);
            ViewData["Nome"] = evento.NomeEvento;
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Cidade)
                .Include(e => e.Entidade)
                .Include(e => e.Estado)
                .Include(e => e.Funcionario)
                .Include(e => e.LocalEvento)
                .Include(e => e.Orcamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SalvarItem(int EquipId, int EventoId, int InstrumentoId, int Qtde, int Dias, string Tipo, int Tabela, decimal? valItem)
        {

            var item = new EventoItem();
            item.EventoId = EventoId;
            item.EquipamentoId = EquipId;
            item.Quantidade = Qtde;
            item.Diarias = Dias;
            var instr = _context.Instrumento.FirstOrDefault(x => x.Id == InstrumentoId);
            if (Tipo == "Sistema")
            {
                var equip = _context.Sistema.Find(EquipId);
                if (valItem > 0)
                {
                    item.ValorTotalItem = Convert.ToDecimal(valItem) * Qtde * Dias;
                    item.Valor = Convert.ToDecimal(valItem);
                }
                else
                {
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.SistemaId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(preco.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    else if (equip.ValorLocacao != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(equip.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(equip.ValorLocacao);
                    }
                }

                item.SistemaId = equip.Id;
                item.Descricao = equip.Descricao;
                item.NomeSistema = equip.Nome;
                if (instr != null)
                {
                    item.InstrumentoId = InstrumentoId;
                    item.NomeInstrumento = instr.Nome;
                }
                
            }
            else if (Tipo == "Case")
            {
                var equip = _context.Case.Find(EquipId);
                if (valItem > 0)
                {
                    item.ValorTotalItem = Convert.ToDecimal(valItem) * Qtde * Dias;
                    item.Valor = Convert.ToDecimal(valItem);
                }
                else
                {
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.CaseId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(preco.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    else if (equip.ValorLocacao != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(equip.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(equip.ValorLocacao);
                    }
                }
                item.CaseId = equip.Id;
                item.Descricao = equip.Descricao;
                item.NomeSistema = equip.Nome;
                if (instr != null)
                {
                    item.InstrumentoId = InstrumentoId;
                    item.NomeInstrumento = instr.Nome;
                }
               
            }
            else if (Tipo == "Equipamento")
            {
                var equip = _context.Equipamento.Find(EquipId);
                if (valItem > 0)
                {
                    item.ValorTotalItem = Convert.ToDecimal(valItem) * Qtde * Dias;
                    item.Valor = Convert.ToDecimal(valItem);
                }
                else
                {
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.EquipamentoId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(preco.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    else if (equip.ValorLocacao != null)
                    {
                        item.ValorTotalItem = Convert.ToDecimal(equip.ValorLocacao) * Qtde * Dias;
                        item.Valor = Convert.ToDecimal(equip.ValorLocacao);
                    }
                }
                item.EquipamentoId = equip.Id;
                item.Descricao = equip.Descricao;
                item.NomeSistema = equip.Nome;
                if (instr != null)
                {
                    item.InstrumentoId = InstrumentoId;
                    item.NomeInstrumento = instr.Nome;
                }
             
            }
            _context.EventoItem.Add(item);
            _context.SaveChanges();

            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("EventoFaturamento", new { EventoId });
        }

        [HttpPost]
        public IActionResult SalvarFaturamento(int EventoId, int FormaPagamentoId, int EntidadeId, DateTime dataPrimeira, Decimal valor)
        {

            var forma = _context.FormaPagamento.Find(FormaPagamentoId);
            if (forma.QuantidadeParcelas > 1)
            {
                var dt = dataPrimeira;
                for (int i = 0; i < forma.QuantidadeParcelas; i++)
                {
                    var item = new MovimentoFinanceiro();
                    item.EventoId = EventoId;
                    item.EntidadeId = EntidadeId;
                    item.FormaPagamentoId = FormaPagamentoId;
                    item.TipoMovimentacao = TipoMovimentacao.Entrada;
                    item.Descricao = "Receita gerada automaticamente";
                    item.DataCadastro = DateTime.Now;
                    item.Status = Status.Ativo;
                    item.PlanoContasId = _context.PlanoContas.FirstOrDefault(x => x.Nome == "Receita Locaçao").Id;
                    item.Pago = false;

                    var valorParcela = valor / forma.QuantidadeParcelas;
                    item.Valor = valorParcela;
                    item.dataVencimento = dt;
                    _context.MovimentoFinanceiro.Add(item);
                    _context.SaveChanges();
                    dt = dt.AddMonths(1);
                }
            }
            else
            {
                var item = new MovimentoFinanceiro();
                item.EventoId = EventoId;
                item.EntidadeId = EntidadeId;
                item.FormaPagamentoId = FormaPagamentoId;
                item.TipoMovimentacao = TipoMovimentacao.Entrada;
                item.Descricao = "Receita gerada automaticamente";
                item.DataCadastro = DateTime.Now;
                item.Status = Status.Ativo;
                item.PlanoContasId = _context.PlanoContas.FirstOrDefault(x => x.Nome == "Receita Locaçao").Id;
                item.Pago = false;
                item.dataVencimento = dataPrimeira;
                item.Valor = valor;
                _context.MovimentoFinanceiro.Add(item);
                _context.SaveChanges();
            }

            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("EventoFaturamento", new { EventoId });
        }


        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.Id == id);
        }
    }
}
