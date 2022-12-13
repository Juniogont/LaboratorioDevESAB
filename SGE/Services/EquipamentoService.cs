using SGE.Data;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Services
{
    public class EquipamentoService
    {
        private readonly SGEMvcContext _context;

        public EquipamentoService(SGEMvcContext context)
        {
            _context = context;
        }

        public List<Equipamento> FindAll()
        {
            return _context.Equipamento.OrderBy(x => x.Nome).ToList();
        }

        public List<Equipamento> FindAllByCase(int? CaseId)
        {
            var eqs = from e in _context.Equipamento
                      where e.CaseId == CaseId
                      orderby e.Nome
                      select new Equipamento
                      {
                          Nome = e.Nome + " - " + e.Codigo,
                          Codigo = e.Codigo,
                          Id = e.Id
                      };

            return eqs.ToList();
        }

        public List<Equipamento> FindAllNomeCodigo()
        {
            var eqs = from e in _context.Equipamento
                      orderby e.Nome
                      select new Equipamento
                      {
                          Nome = e.Nome + " - " + e.Codigo,
                          Codigo = e.Codigo,
                          Id = e.Id
                      };

            return eqs.ToList();
        }

        public List<Equipamento> FindSistemasNomeCodigo()
        {
            var eqs = from e in _context.Sistema
                      orderby e.Nome
                      select new Equipamento
                      {
                          Nome = e.Nome + " - " + e.Codigo,
                          Codigo = e.Codigo,
                          Id = e.Id
                      };

            return eqs.ToList();
        }

        public List<Equipamento> FindCasesNomeCodigo()
        {
            var eqs = from e in _context.Case
                      orderby e.Nome
                      select new Equipamento
                      {
                          Nome = e.Nome + " - " + e.Codigo,
                          Codigo = e.Codigo,
                          Id = e.Id
                      };

            return eqs.ToList();
        }


        public List<Equipamento> FindEquipamentosSemCaseAll(int? CaseId)
        {
            var eqs = from ce in _context.CaseEquipamentos  join
                      e in _context.Equipamento on ce.EquipamentoId equals e.Id
                      where ce.CaseId != CaseId
                      orderby e.Nome, e.Codigo
                      select new Equipamento
                      {
                          Nome = e.Nome + " - " + e.Codigo,
                          Codigo = e.Codigo,
                          Id = e.Id
                      };

            return eqs.ToList();
        }
    }
}
