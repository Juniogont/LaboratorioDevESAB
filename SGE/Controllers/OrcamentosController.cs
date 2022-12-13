using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;


namespace SGE.Controllers
{
    [Authorize]
    public class OrcamentosController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly EquipamentoService _equipamentoService;
        private readonly InstrumentoService _instrumentoService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        const int itensPorPagina = 10;
        public OrcamentosController(SGEMvcContext context,
            EquipamentoService equipamentoService,
            InstrumentoService instrumentoService,
            IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _equipamentoService = equipamentoService;
            _instrumentoService = instrumentoService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Orcamentos
        public async Task<IActionResult> Index(int? pagina)
        {
            int numeroPagina = (pagina ?? 1);
            //var SGEMvcContext = _context.Orcamento.Include(o => o.Cidade).Include(o => o.Entidade).Include(o => o.Estado).Include(o => o.LocalEvento).Include(o => o.ModeloOrcamento);
            //return View(await SGEMvcContext.ToListAsync());

            var resultado = new ViewModelOrcamento();
            resultado.Orcamentos = await _context.Orcamento.Include(o => o.Cidade).Include(o => o.Entidade).Include(o => o.Estado).Include(o => o.LocalEvento).Include(o => o.ModeloOrcamento).OrderByDescending(x => x.DataCadastro).ToPagedListAsync(numeroPagina, itensPorPagina);
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(ViewModelOrcamento v, int? pagina)
        {
            int numeroPagina = (pagina ?? 1);
            var resultado = new ViewModelOrcamento();
            if (!string.IsNullOrEmpty(v.buscaNome))
                resultado.Orcamentos = await _context.Orcamento.Where(x => x.NomeSolicitante.Contains(v.buscaNome) || x.NumeroProposta.Contains(v.buscaNome) || x.Cidade.Nome.Contains(v.buscaNome) || x.Estado.Sigla.Contains(v.buscaNome)).OrderByDescending(x => x.DataCadastro).ToPagedListAsync(numeroPagina, itensPorPagina);
            else
                resultado.Orcamentos = await _context.Orcamento.OrderByDescending(x => x.DataCadastro).ToPagedListAsync(numeroPagina, itensPorPagina);

            return View("Index", resultado);
        }


        public ActionResult ViewPartialEntidade()
        {
            ViewData["EstadoId"] = _context.Estado.ToList();
            return PartialView();
        }


        [HttpPost]
        public IActionResult SalvarItem(int EquipId, int OrcamentoId, int InstrumentoId, int Qtde, int Dias, string Tipo, int Tabela, decimal? valItem, string descricaoItem)
        {

            var item = new OrcamentoItem();
            item.OrcamentoId = OrcamentoId;
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
                item.Descricao = descricaoItem;
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
            _context.OrcamentoItem.Add(item);
            _context.SaveChanges();

            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("OrcamentoItens", new { OrcamentoId });
        }


        [HttpPost]
        public IActionResult ExcluirItem(int EquipId, int OrcamentoId)
        {

            var item = _context.OrcamentoItem.Find(EquipId);
            _context.OrcamentoItem.Remove(item);
            _context.SaveChanges();

            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("OrcamentoItens", new { OrcamentoId });
        }


        //[HttpPost]
        //public ActionResult ConvertHtmlToPdf(IFormCollection collection)
        //{
        //    // Get the server IP and port
        //    String serverIP = collection["textBoxServerIP"];
        //    uint serverPort = uint.Parse(collection["textBoxServerPort"]);

        //    // Create a HTML to PDF converter object
        //    //HtmlToPdfConverter htmlToPdfConverter = null;
        //    //if (collection["ServerType"] == "radioButtonUseTcpService")
        //    //    htmlToPdfConverter = new HtmlToPdfConverter(serverIP, serverPort);
        //    //else
        //    //    htmlToPdfConverter = new HtmlToPdfConverter(true, collection["textBoxWebServiceUrl"]);

        //    // Set optional service password
        //    if (collection["textBoxServicePassword"][0].Length > 0)
        //        htmlToPdfConverter.ServicePassword = collection["textBoxServicePassword"];

        //    // Set license key received after purchase to use the converter in licensed mode
        //    // Leave it not set to use the converter in demo mode
        //    htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

        //    // Set HTML Viewer width in pixels which is the equivalent in converter of the browser window width
        //    htmlToPdfConverter.HtmlViewerWidth = int.Parse(collection["htmlViewerWidthTextBox"]);

        //    // Set HTML viewer height in pixels to convert the top part of a HTML page 
        //    // Leave it not set to convert the entire HTML
        //    if (collection["htmlViewerHeightTextBox"][0].Length > 0)
        //        htmlToPdfConverter.HtmlViewerHeight = int.Parse(collection["htmlViewerHeightTextBox"]);

        //    // Set PDF page size which can be a predefined size like A4 or a custom size in points 
        //    // Leave it not set to have a default A4 PDF page
        //    htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = SelectedPdfPageSize(collection["pdfPageSizeDropDownList"]);

        //    // Set PDF page orientation to Portrait or Landscape
        //    // Leave it not set to have a default Portrait orientation for PDF page
        //    htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = SelectedPdfPageOrientation(collection["pdfPageOrientationDropDownList"]);

        //    // Set the maximum time in seconds to wait for HTML page to be loaded 
        //    // Leave it not set for a default 60 seconds maximum wait time
        //    htmlToPdfConverter.NavigationTimeout = int.Parse(collection["navigationTimeoutTextBox"]);

        //    // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
        //    // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
        //    if (collection["conversionDelayTextBox"][0].Length > 0)
        //        htmlToPdfConverter.ConversionDelay = int.Parse(collection["conversionDelayTextBox"]);

        //    // The buffer to receive the generated PDF document
        //    byte[] outPdfBuffer = null;

        //    if (collection["HtmlPageSource"] == "convertUrlRadioButton")
        //    {

        //        string url = collection["urlTextBox"];

        //        // Convert the HTML page given by an URL to a PDF document in a memory buffer
        //        outPdfBuffer = htmlToPdfConverter.ConvertUrl(url);
        //    }
        //    else
        //    {
        //        string htmlString = collection["htmlStringTextBox"];
        //        string baseUrl = collection["baseUrlTextBox"];

        //        // Convert a HTML string with a base URL to a PDF document in a memory buffer
        //        outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlString, baseUrl);
        //    }

        //    // Send the PDF file to browser
        //    FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
        //    if (collection["openInlineCheckBox"].Count == 0)
        //    {
        //        // send as attachment
        //        fileResult.FileDownloadName = "Getting_Started.pdf";
        //    }

        //    return fileResult;
        //}


        public IActionResult GerarEvento(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var orcamento = _context.Orcamento.Find(id);
            if (orcamento == null)
            {
                return NotFound();
            }

            var e = new Evento();
            e.CidadeId = orcamento.CidadeId;
            e.CondicoesPagamento = orcamento.CondicoesPagamento;
            e.DataAprovacao = DateTime.Today;
            e.DataCadastro = DateTime.Today;
            e.dataInicio = orcamento.dataInicio;
            e.dataFim = orcamento.dataFim;
            e.dataMontagem = orcamento.dataMontagem;
            e.dataProposta = orcamento.dataProposta;
            e.Email = orcamento.Email;
            e.EmpresaSolicitante = orcamento.EmpresaSolicitante;
            e.EntidadeId = orcamento.EntidadeId;
            e.Entrega = orcamento.Entrega;
            e.EstadoId = orcamento.EstadoId;
            e.LocalEventoId = orcamento.LocalEventoId;
            e.MostrarValoresUnitarios = orcamento.MostrarValoresUnitarios;
            e.MostrarValorTotal = orcamento.MostrarValorTotal;
            e.NomeEvento = orcamento.NomeEvento;
            e.NomeSolicitante = orcamento.NomeSolicitante;
            e.NumeroProposta = orcamento.NumeroProposta;
            var ultimNum = _context.Evento.Max(x => x.NumeroOS);
            if (!string.IsNullOrEmpty(ultimNum))
                e.NumeroOS = (Convert.ToInt32(ultimNum) + 1).ToString();
            else
                e.NumeroOS = "1";
            e.Observacoes = orcamento.Observacoes;
            e.ObservacoesChecklist = orcamento.ObservacoesChecklist;
            e.OrcamentoId = Convert.ToInt32(id);
            e.SituacaoEvento = SituacaoEvento.Aprovado;
            e.Status = Status.Ativo;
            e.Telefone = orcamento.Telefone;
            e.TipoProposta = orcamento.TipoProposta;
            e.UsuarioCadastro = orcamento.UsuarioCadastro;
            e.ValidadeProposta = orcamento.ValidadeProposta;
            e.ValorDesconto = orcamento.ValorDesconto;
            e.ValorTotal = orcamento.ValorTotal;
            using (var _db = new SGEMvcContext())
            {
                _db.Evento.Add(e);
                _db.SaveChanges();
            }
            if (e.Id > 0)
            {
                var lstItens = from oItens in _context.OrcamentoItem.Where(x => x.OrcamentoId == id) select oItens;

                foreach (var item in lstItens)
                {
                    var dbCont = new DbContextOptions<SGEMvcContext>();

                    using (var _db = new SGEMvcContext())
                    {
                        var i = new EventoItem();
                        i.CaseId = item.CaseId;
                        i.Descricao = item.Descricao;
                        i.Diarias = item.Diarias;
                        i.EquipamentoId = item.EquipamentoId;
                        i.EventoId = e.Id;
                        i.InstrumentoId = item.InstrumentoId;
                        i.NomeInstrumento = item.NomeInstrumento;
                        i.NomeSistema = item.NomeSistema;
                        i.Quantidade = item.Quantidade;
                        i.SistemaId = item.SistemaId;
                        i.Valor = item.Valor;
                        i.ValorSublocacao = item.ValorSublocacao;
                        i.ValorTotalItem = item.ValorTotalItem;
                        _db.EventoItem.Add(i);
                        _db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Edit", "Eventos", new { id = e.Id });
        }


        // GET: Orcamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamento = await _context.Orcamento
                .Include(o => o.Cidade)
                .Include(o => o.Entidade)
                .Include(o => o.Estado)
                .Include(o => o.LocalEvento)
                .Include(o => o.ModeloOrcamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamento == null)
            {
                return NotFound();
            }
            var instrumentos = new List<string>();
            using (var _db = new SGEMvcContext())
            {
                var lstI = from o in _context.OrcamentoItem
                               .Where(x => x.OrcamentoId == id).Distinct()
                           select o.NomeInstrumento;
                instrumentos = lstI.ToList();
            }

            var viewimprimir = new ViewModelOrcamentoImprimir();
            if (orcamento.ModeloOrcamento != null && orcamento.ModeloOrcamento.LogomarcaPath != null)
                viewimprimir.Logomarca = string.Format("/images/{0}", orcamento.ModeloOrcamento.LogomarcaPath);

            var lstInstrum = new List<ViewModelOrcamentoItens>();
            var soma = 0.00M;
            foreach (var item in instrumentos)
            {
                var obj = new ViewModelOrcamentoItens();
                if (!string.IsNullOrEmpty(item))
                {
                    obj.Instrumento = item.ToUpper();
                    obj.Itens = await _context.OrcamentoItem.Where(x => x.OrcamentoId == id && x.NomeInstrumento == item).ToListAsync();
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
            }

            viewimprimir.Orcamento = orcamento;
            viewimprimir.Intrumentos = lstInstrum;

            ViewData["LocalEData"] = string.Format("Curitiba, {0}", orcamento.dataProposta.ToLongDateString());
            ViewData["Proposta"] = string.Format("{0}/{1}", orcamento.NumeroProposta, orcamento.dataProposta.ToString("yy"));
            ViewData["DataInicio"] = orcamento.dataInicio.ToShortDateString();
            ViewData["HoraInicio"] = orcamento.dataInicio.ToShortTimeString();
            ViewData["DataFim"] = orcamento.dataFim.ToShortDateString();
            ViewData["HoraFim"] = orcamento.dataFim.ToShortTimeString();
            var ext = new ClsExtenso();
            var somaStr = ext.Extenso_Valor(soma);
            ViewData["ValorTotal"] = String.Format("Valor Total: {0}({1})", soma.ToString("C2"), somaStr);
            ViewData["Assinatura"] = orcamento.ModeloOrcamento.Assinatura;
            return View(viewimprimir);

        }

        public IActionResult GerarPDF()
        {

            //HtmlToPdfConverter converter = new HtmlToPdfConverter();
            //WebKitConverterSettings settings = new WebKitConverterSettings();
            //settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
            //converter.ConverterSettings = settings;



            //PdfDocument document = converter.Convert("https://localhost:44309/Orcamentos/Details/2");

            //MemoryStream ms = new MemoryStream();
            //document.Save(ms);
            //ms.Position = 0;
            //FileStreamResult fsr = new FileStreamResult(ms, "application/pdf");
            //fsr.FileDownloadName = "teste.pdf";
            //return fsr;
            ////if (id == null)
            ////{
            ////    return NotFound();
            //}

            //var orcamento = await _context.Orcamento
            //    .Include(o => o.Cidade)
            //    .Include(o => o.Entidade)
            //    .Include(o => o.Estado)
            //    .Include(o => o.LocalEvento)
            //    .Include(o => o.ModeloOrcamento)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (orcamento == null)
            //{
            //    return NotFound();
            //}

            //var itens = await _context.OrcamentoItem.Where(x => x.OrcamentoId == id).ToListAsync();
            //var viewimprimir = new ViewModelOrcamentoImprimir();
            //if(orcamento.ModeloOrcamento != null && orcamento.ModeloOrcamento.Logomarca !=null)
            //{
            //    MemoryStream ms = new MemoryStream(orcamento.ModeloOrcamento.Logomarca);
            //    viewimprimir.Logomarca = new FileStreamResult(ms, orcamento.ModeloOrcamento.ContentType);
            //}
            //viewimprimir.Orcamento = orcamento;
            //viewimprimir.Itens = itens;
            return View();

          
        }





        // GET: Orcamentos/Create
        public IActionResult Create()
        {
            var orca = new Orcamento();
            orca.ValorDesconto = 0;
            var ultimNum = _context.Orcamento.Max(x => x.NumeroProposta);
            if (!string.IsNullOrEmpty(ultimNum))
                orca.NumeroProposta = (Convert.ToInt32(ultimNum) + 1).ToString().PadLeft(4,'0');
            else
                orca.NumeroProposta = "0001";
            orca.CondicoesPagamento = "50% na assinatura / 50% até a data do evento.";
            orca.Entrega = "A combinar.";
            orca.ValidadeProposta = "5 (cinco) dias.";
            orca.dataInicio = DateTime.Today.AddDays(1);
            orca.dataFim = DateTime.Today.AddDays(1);
            orca.dataMontagem = DateTime.Today.AddDays(1);
            orca.dataProposta = DateTime.Today;
            ViewData["EntidadeId"] = _context.Entidade.OrderBy(x => x.Nome).Where(x => x.Cliente == true).ToList();
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["ModeloOrcamentoId"] = new SelectList(_context.ModeloOrcamento, "Id", "Nome");
            return View(orca);
        }

        // POST: Orcamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeloOrcamentoId,NumeroProposta,NomeEvento,LocalEventoId,SituacaoOrcamento,dataProposta,dataInicio,dataFim,dataMontagem,HoraInicioEvento,HoraFimEvento,HoraInicioMontagem,HoraFimMontagem,HoraInicioDesmontagem,HoraFimDesmontagem,TipoProposta,EntidadeId,NomeSolicitante,EmpresaSolicitante,Telefone,Email,EstadoId,CidadeId,MostrarValoresUnitarios,MostrarValorTotal,ValorTotal,ValorDesconto,CondicoesPagamento,Entrega,ValidadeProposta,Observacoes,ObservacoesChecklist,Id")] Orcamento orcamento)
        {

            if (ModelState.IsValid)
            {
                var ultimNum = _context.Orcamento.Max(x => x.NumeroProposta);
                if (!string.IsNullOrEmpty(ultimNum))
                    orcamento.NumeroProposta = (Convert.ToInt32(ultimNum) + 1).ToString();
                else
                    orcamento.NumeroProposta = "1";
                orcamento.DataCadastro = DateTime.Now;
                orcamento.Status = Status.Ativo;
                orcamento.SituacaoOrcamento = SituacaoOrcamento.Novo;
                _context.Add(orcamento);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Edit), new { id = orcamento.Id });
            }
            ViewData["EntidadeId"] = _context.Entidade.OrderBy(x => x.Nome).Where(x => x.Cliente == true).ToList();
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["LocalEventoId"] = _context.LocalEvento.ToList();
            ViewData["ModeloOrcamentoId"] = new SelectList(_context.ModeloOrcamento, "Id", "Nome", orcamento.ModeloOrcamentoId);
            return View(orcamento);
        }
        // GET: Orcamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamento = await _context.Orcamento.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }



            ViewData["TipoItem"] = "TipoSistema";
            ViewData["NomeBtnItens"] = "Incluir Sistema";
            ViewData["OrcamentoId"] = id;
            ViewData["Equipamentos"] = _equipamentoService.FindSistemasNomeCodigo();
            ViewData["Instrumentos"] = _instrumentoService.FindAll();
            ViewData["EntidadeId"] = new SelectList(_context.Entidade.OrderBy(x => x.Nome).Where(x => x.Cliente == true), "Id", "Nome", orcamento.LocalEventoId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Sigla", orcamento.EstadoId);
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", orcamento.CidadeId);
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Nome", orcamento.LocalEventoId);
            ViewData["ModeloOrcamentoId"] = new SelectList(_context.ModeloOrcamento, "Id", "Nome", orcamento.ModeloOrcamentoId);
            return View(orcamento);
        }

        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModeloOrcamentoId,NumeroProposta,NomeEvento,LocalEventoId,SituacaoOrcamento,dataProposta,dataInicio,dataFim,dataMontagem,HoraInicioEvento,HoraFimEvento,HoraInicioMontagem,HoraFimMontagem,HoraInicioDesmontagem,HoraFimDesmontagem,TipoProposta,EntidadeId,NomeSolicitante,EmpresaSolicitante,Telefone,Email,EstadoId,CidadeId,MostrarValoresUnitarios,MostrarValorTotal,ValorTotal,ValorDesconto,CondicoesPagamento,Entrega,ValidadeProposta,Observacoes,ObservacoesChecklist,Id,Status")] Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orcamento.DataAlteracao = DateTime.Now;
                    _context.Update(orcamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoExists(orcamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }               
            }
            ViewData["TipoItem"] = "TipoSistema";
            ViewData["NomeBtnItens"] = "Incluir Sistema";
            ViewData["OrcamentoId"] = id;
            ViewData["Equipamentos"] = _equipamentoService.FindSistemasNomeCodigo();
            ViewData["Instrumentos"] = _instrumentoService.FindAll();
            ViewData["EntidadeId"] = new SelectList(_context.Entidade.OrderBy(x => x.Nome).Where(x => x.Cliente == true), "Id", "Nome", orcamento.LocalEventoId);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "Id", "Sigla", orcamento.EstadoId);
            ViewData["CidadeId"] = new SelectList(_context.Cidade, "Id", "Nome", orcamento.CidadeId);
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Nome", orcamento.LocalEventoId);
            ViewData["ModeloOrcamentoId"] = new SelectList(_context.ModeloOrcamento, "Id", "Nome", orcamento.ModeloOrcamentoId);
            return View(orcamento);
        }


        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarSair(int id, [Bind("ModeloOrcamentoId,NumeroProposta,NomeEvento,LocalEventoId,SituacaoOrcamento,dataProposta,dataInicio,dataFim,dataMontagem,HoraInicioEvento,HoraFimEvento,HoraInicioMontagem,HoraFimMontagem,HoraInicioDesmontagem,HoraFimDesmontagem,TipoProposta,EntidadeId,NomeSolicitante,EmpresaSolicitante,Telefone,Email,EstadoId,CidadeId,MostrarValoresUnitarios,MostrarValorTotal,ValorTotal,ValorDesconto,CondicoesPagamento,Entrega,ValidadeProposta,Observacoes,ObservacoesChecklist,Id,Status")] Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orcamento.DataAlteracao = DateTime.Now;
                    _context.Update(orcamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoExists(orcamento.Id))
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
            ViewData["OrcamentoId"] = id;
            ViewData["EntidadeId"] = new SelectList(_context.Entidade.OrderBy(x => x.Nome).Where(x => x.Cliente == true), "Id", "Nome", orcamento.LocalEventoId);
            ViewData["EstadoId"] = _context.Estado.ToList();
            ViewData["Equipamentos"] = _equipamentoService.FindSistemasNomeCodigo();
            ViewData["Instrumentos"] = _instrumentoService.FindAll();
            ViewData["LocalEventoId"] = new SelectList(_context.LocalEvento, "Id", "Nome", orcamento.LocalEventoId);
            ViewData["ModeloOrcamentoId"] = new SelectList(_context.ModeloOrcamento, "Id", "Nome", orcamento.ModeloOrcamentoId);
            return RedirectToAction(nameof(Index));
        }


        // GET: Orcamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orcamento = await _context.Orcamento
                .Include(o => o.Cidade)
                .Include(o => o.Entidade)
                .Include(o => o.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamento == null)
            {
                return NotFound();
            }

            return View(orcamento);
        }

        // POST: Orcamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orcamento = await _context.Orcamento.FindAsync(id);
            _context.Orcamento.Remove(orcamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool OrcamentoExists(int id)
        {
            return _context.Orcamento.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarItens(int EquipId, int OrcamentoId)
        {

            var oItem = new OrcamentoItem();
            oItem.OrcamentoId = OrcamentoId;
            oItem.EquipamentoId = EquipId;
            oItem.Quantidade = 1;
            oItem.Diarias = 1;
            oItem.Valor = 100;
            _context.OrcamentoItem.Add(oItem);
            await _context.SaveChangesAsync();
            ViewData["OrcamentoId"] = OrcamentoId;
            TempData["salvouEquip"] = "SIM";
            ViewBag.classeDadosTab = "";
            ViewBag.classeDetalhesTab = "active";
            ViewBag.Msg = "Equipamento incluido com sucesso";
            //var model = await _context.Case.FindAsync(Caseid);
            //model.Equipamentos = _context.CaseEquipamentos.Where(x => x.CaseId == model.Id).Include(s => s.Equipamento).ToList();
            //return "Equipamento incluido com sucesso";
            //return RedirectToAction(nameof(Edit), Caseid);
            //return PartialView("ViewCaseEquipamentos", model);
            return ViewComponent("OrcamentoItens", new { OrcamentoId = OrcamentoId });
        }

    }
}
