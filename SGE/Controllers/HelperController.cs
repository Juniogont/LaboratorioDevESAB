using Microsoft.AspNetCore.Mvc;
using SGE.Data;
using SGE.Models;
using System;
using System.Linq;

namespace SGE.Controllers
{
    public class HelperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CidadesByEstado(int id)
        {
            using (var _context = new SGEMvcContext())
            {
                // Filter the states by country. For example:
                var cidades = (from s in _context.Cidade
                               where s.IdEstado == id
                               select new
                               {
                                   id = s.Id,
                                   state = s.Nome
                               }).ToArray();

                return Json(cidades);
            }

        }

        [HttpPost]
        public ActionResult CarregarClientes(TipoProposta tipo)
        {
            using (var _context = new SGEMvcContext())
            {
                if (tipo == TipoProposta.Cliente)
                {
                    // Filter the states by country. For example:
                    var clientes = (from s in _context.Entidade
                                    where s.Cliente == true
                                    orderby s.Nome
                                    select new
                                    {
                                        id = s.Id,
                                        state = s.Nome
                                    }).ToArray();

                    return Json(clientes);
                }
                else
                    return Json("");
            }
        }

        [HttpPost]
        public ActionResult GetDadosCliente(int id)
        {
            using (var _context = new SGEMvcContext())
            {
                if (id > 0)
                {
                    // Filter the states by country. For example:
                    var cliente = _context.Entidade.FirstOrDefault(x => x.Id == id);
                    return Json(cliente);
                }
                else
                    return Json("");
            }
        }

        [HttpPost]
        public ActionResult CarregarCases()
        {
            using (var _context = new SGEMvcContext())
            {
                // Filter the states by country. For example:
                var cases = (from s in _context.Case
                             where s.Status == Status.Ativo
                             select new
                             {
                                 id = s.Id,
                                 state = s.Codigo + " - " + s.Nome
                             }).ToArray();

                ViewData["TipoItem"] = "TipoCase";

                return Json(cases);
            }
        }

        [HttpPost]
        public ActionResult CarregarSistemas()
        {
            using (var _context = new SGEMvcContext())
            {
                // Filter the states by country. For example:
                var cases = (from s in _context.Sistema
                             where s.Status == Status.Ativo
                             select new
                             {
                                 id = s.Id,
                                 state = s.Codigo + " - " + s.Nome
                             }).ToArray();



                return Json(cases);
            }

        }

        [HttpPost]
        public ActionResult CarregarEquipamentos()
        {
            using (var _context = new SGEMvcContext())
            {
                // Filter the states by country. For example:
                var cases = (from s in _context.Equipamento
                             where s.Status == Status.Ativo
                             select new
                             {
                                 id = s.Id,
                                 state = s.Codigo + " - " + s.Nome
                             }).ToArray();



                return Json(cases);
            }
        }

        [HttpPost]
        public IActionResult GetInstrumento(int EquipId, string Tipo)
        {
            using (var _context = new SGEMvcContext())
            {
                if (Tipo == "Sistema")
                {
                    var equip = _context.Sistema.Find(EquipId);

                    if (equip.InstrumentoId != null)
                    {
                        return Json(equip.InstrumentoId);
                    }
                }
                else if (Tipo == "Case")
                {
                    return Json(0);
                }
                else if (Tipo == "Equipamento")
                {
                    var equip = _context.Equipamento.Find(EquipId);

                    if (equip != null)
                    {
                        return Json(equip.InstrumentoId);
                    }
                }
                return null;
            }
        }

        [HttpPost]
        public IActionResult GetInstrumentoComValor(int EquipId, string Tipo, short Tabela)
        {
            using (var _context = new SGEMvcContext())
            {
                var icv = new IntrumentoComValor();
                if (Tipo == "Sistema")
                {                 
                    var equip = _context.Sistema.Find(EquipId);
                    if (equip.InstrumentoId != null)
                    {
                        icv.intrumento = equip.InstrumentoId;
                       
                    }
                    if (!string.IsNullOrEmpty(equip.Descricao))
                        icv.descricaoItem = equip.Descricao;//.Replace("\n",",");
                    else
                        icv.descricaoItem = equip.Nome;
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.SistemaId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        icv.valorItem = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    return Json(icv);
                }
                else if (Tipo == "Case")
                {
                    var equip = _context.Case.Find(EquipId);
                    icv.intrumento = 0;
                    if (!string.IsNullOrEmpty(equip.Descricao))
                        icv.descricaoItem = equip.Descricao;
                    else
                        icv.descricaoItem = equip.Nome;
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.CaseId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        icv.valorItem = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    return Json(icv);
                }
                else if (Tipo == "Equipamento")
                {
                    var equip = _context.Equipamento.Find(EquipId);
                    if (equip != null)
                    {
                        icv.intrumento = equip.InstrumentoId;
                    }
                    if (!string.IsNullOrEmpty(equip.Descricao))
                        icv.descricaoItem = equip.Descricao;
                    else
                        icv.descricaoItem = equip.Nome;
                    var preco = _context.TabelaPreco.FirstOrDefault(x => x.EquipamentoId == EquipId && x.Tabela == Tabela);
                    if (preco != null)
                    {
                        icv.valorItem = Convert.ToDecimal(preco.ValorLocacao);
                    }
                    return Json(icv);
                }
                return null;
            }
        }

        [HttpPost]
        public IActionResult GetValue(int Id)
        {
            return Json(Id);
        }


    }

    public class IntrumentoComValor
    {
        public int? intrumento { get; set; }
        public decimal? valorItem { get; set; }
        public string descricaoItem { get; set; }
    }
}