using SGE.Data;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Services
{
    public class TabelaPrecoService
    {
        private readonly SGEMvcContext _context;

        public TabelaPrecoService(SGEMvcContext context)
        {
            _context = context;
        }


        public List<TabelaPreco> FindAllSistemas(short? tabela, string nome)
        {
            if (tabela == null)
                tabela = 1;
            var retorno = new List<TabelaPreco>();
            List<Sistema> eqs;
            if (string.IsNullOrEmpty(nome))
                eqs = _context.Sistema.Where(x => x.Status == Status.Ativo).ToList();
            else
                eqs = _context.Sistema.Where(x => x.Status == Status.Ativo && x.Nome.Contains(nome)).ToList();
            var tbs = _context.TabelaPreco.Where(x => x.Status == Status.Ativo && x.SistemaId != null && x.Tabela == tabela).ToList();

            foreach (var eq in eqs)
            {
                var t = new TabelaPreco();
                t.Nome = eq.Nome;
                t.SistemaId = eq.Id;
                t.Tabela = Convert.ToInt16(tabela);
                var tb = tbs.FirstOrDefault(x => x.SistemaId == eq.Id);
                if (tb != null)
                {
                    t.Id = tb.Id;
                    t.ValorCompra = tb.ValorCompra;
                    t.ValorLocacao = tb.ValorLocacao;
                }
                else
                {
                    t.Id = 0;
                    t.ValorCompra = 0;
                    t.ValorLocacao = 0;
                }
                retorno.Add(t);
            }


            return retorno;
        }

        public List<TabelaPreco> FindAllCases(short? tabela, string nome)
        {
            if (tabela == null)
                tabela = 1;
            var retorno = new List<TabelaPreco>();
            List<Case> eqs;
            if (string.IsNullOrEmpty(nome))
                eqs = _context.Case.Where(x => x.Status == Status.Ativo).ToList();
            else
                eqs = _context.Case.Where(x => x.Status == Status.Ativo && x.Nome.Contains(nome)).ToList();
            var tbs = _context.TabelaPreco.Where(x => x.Status == Status.Ativo && x.SistemaId != null && x.Tabela == tabela).ToList();

            foreach (var eq in eqs)
            {
                var t = new TabelaPreco();
                t.Nome = eq.Nome;
                t.CaseId = eq.Id;
                t.Tabela = Convert.ToInt16(tabela);
                var tb = tbs.FirstOrDefault(x => x.CaseId == eq.Id);
                if (tb != null)
                {
                    t.Id = tb.Id;
                    t.ValorCompra = tb.ValorCompra;
                    t.ValorLocacao = tb.ValorLocacao;
                }
                else
                {
                    t.Id = 0;
                    t.ValorCompra = 0;
                    t.ValorLocacao = 0;
                }
                retorno.Add(t);
            }


            return retorno;
        }

        public List<TabelaPreco> FindAllEquipamentos(short? tabela, string nome)
        {
            if (tabela == null)
                tabela = 1;
            var retorno = new List<TabelaPreco>();
            List<Equipamento> eqs;
            if (string.IsNullOrEmpty(nome))
                eqs = _context.Equipamento.Where(x => x.Status == Status.Ativo).ToList();
            else
                eqs = _context.Equipamento.Where(x => x.Status == Status.Ativo && x.Nome.Contains(nome)).ToList();
            var tbs = _context.TabelaPreco.Where(x => x.Status == Status.Ativo && x.SistemaId != null && x.Tabela == tabela).ToList();

            foreach (var eq in eqs)
            {
                var t = new TabelaPreco();
                t.Nome = eq.Nome;
                t.EquipamentoId = eq.Id;
                t.Tabela = Convert.ToInt16(tabela);
                var tb = tbs.FirstOrDefault(x => x.EquipamentoId == eq.Id);
                if (tb != null)
                {
                    t.Id = tb.Id;
                    t.ValorCompra = tb.ValorCompra;
                    t.ValorLocacao = tb.ValorLocacao;
                }
                else
                {
                    t.Id = 0;
                    t.ValorCompra = 0;
                    t.ValorLocacao = 0;
                }
                retorno.Add(t);
            }


            return retorno;
        }


        public List<TabelaPreco> UpdateAllSistemas(short? tabela, decimal? percentual)
        {
            if (tabela == null)
                tabela = 1;
            if (percentual == null)
                percentual = 1;
            var retorno = new List<TabelaPreco>();
            var eqs = _context.Sistema.Where(x => x.Status == Status.Ativo).ToList();            

            foreach (var eq in eqs)
            {
                var tb = _context.TabelaPreco.FirstOrDefault(x => x.SistemaId == eq.Id && x.Tabela == tabela);

                if (tb != null)
                {                    
                    //tb.ValorCompra = tb.ValorCompra + ( (tb.ValorCompra /100) * percentual);
                    tb.ValorLocacao = tb.ValorLocacao + ((tb.ValorLocacao / 100) * percentual);
                    tb.Status = Status.Ativo;
                    _context.Update(tb);
                    _context.SaveChanges();
                    retorno.Add(tb);
                }
                else
                {
                    TabelaPreco tb1 = null; 
                    if (tabela > 1)
                        tb1 = _context.TabelaPreco.FirstOrDefault(x => x.SistemaId == eq.Id && x.Tabela == 1);
                    var t = new TabelaPreco();
                    t.Nome = eq.Nome;
                    t.SistemaId = eq.Id;
                    t.Tabela = Convert.ToInt16(tabela);
                    if (tb1 != null)
                    {
                        t.ValorCompra = tb1.ValorCompra;
                        t.ValorLocacao = tb1.ValorLocacao + ((tb1.ValorLocacao / 100) * percentual);
                    }
                    else
                    {
                        t.ValorCompra = 200;
                        t.ValorLocacao = 100;
                    }
                    t.Status = Status.Ativo;
                    t.DataCadastro = DateTime.Now;
                    _context.TabelaPreco.Add(t);
                    _context.SaveChanges();
                    retorno.Add(t);
                }                
            }

            return retorno;
        }

        public List<TabelaPreco> UpdateAllCases(short? tabela, decimal? percentual)
        {
            if (tabela == null)
                tabela = 1;
            if (percentual == null)
                percentual = 1;
            var retorno = new List<TabelaPreco>();
            var eqs = _context.Case.Where(x => x.Status == Status.Ativo).ToList();

            foreach (var eq in eqs)
            {
                var tb = _context.TabelaPreco.FirstOrDefault(x => x.CaseId == eq.Id && x.Tabela == tabela);

                if (tb != null)
                {
                    //tb.ValorCompra = tb.ValorCompra + ( (tb.ValorCompra /100) * percentual);
                    tb.ValorLocacao = tb.ValorLocacao + ((tb.ValorLocacao / 100) * percentual);
                    tb.Status = Status.Ativo;
                    _context.Update(tb);
                    _context.SaveChanges();
                    retorno.Add(tb);
                }
                else
                {
                    TabelaPreco tb1 = null;
                    if (tabela > 1)
                        tb1 = _context.TabelaPreco.FirstOrDefault(x => x.CaseId == eq.Id && x.Tabela == 1);
                    var t = new TabelaPreco();
                    t.Nome = eq.Nome;
                    t.CaseId = eq.Id;
                    t.Tabela = Convert.ToInt16(tabela);
                    if (tb1 != null)
                    {
                        t.ValorCompra = tb1.ValorCompra;
                        t.ValorLocacao = tb1.ValorLocacao + ((tb1.ValorLocacao / 100) * percentual);
                    }
                    else
                    {
                        t.ValorCompra = 200;
                        t.ValorLocacao = 100;
                    }
                    t.Status = Status.Ativo;
                    t.DataCadastro = DateTime.Now;
                    _context.TabelaPreco.Add(t);
                    _context.SaveChanges();
                    retorno.Add(t);
                }
            }

            return retorno;
        }

        public List<TabelaPreco> UpdateAllEquipamentos(short? tabela, decimal? percentual)
        {
            if (tabela == null)
                tabela = 1;
            if (percentual == null)
                percentual = 1;
            var retorno = new List<TabelaPreco>();
            var eqs = _context.Equipamento.Where(x => x.Status == Status.Ativo).ToList();

            foreach (var eq in eqs)
            {
                var tb = _context.TabelaPreco.FirstOrDefault(x => x.EquipamentoId == eq.Id && x.Tabela == tabela);

                if (tb != null)
                {
                    //tb.ValorCompra = tb.ValorCompra + ( (tb.ValorCompra /100) * percentual);
                    tb.ValorLocacao = tb.ValorLocacao + ((tb.ValorLocacao / 100) * percentual);
                    tb.Status = Status.Ativo;
                    _context.Update(tb);
                    _context.SaveChanges();
                    retorno.Add(tb);
                }
                else
                {
                    TabelaPreco tb1 = null;
                    if (tabela > 1)
                        tb1 = _context.TabelaPreco.FirstOrDefault(x => x.EquipamentoId == eq.Id && x.Tabela == 1);
                    var t = new TabelaPreco();
                    t.Nome = eq.Nome;
                    t.EquipamentoId = eq.Id;
                    t.Tabela = Convert.ToInt16(tabela);
                    if (tb1 != null)
                    {
                        t.ValorCompra = tb1.ValorCompra;
                        t.ValorLocacao = tb1.ValorLocacao + ((tb1.ValorLocacao / 100) * percentual);
                    }
                    else
                    {
                        t.ValorCompra = 200;
                        t.ValorLocacao = 100;
                    }
                    t.Status = Status.Ativo;
                    t.DataCadastro = DateTime.Now;
                    _context.TabelaPreco.Add(t);
                    _context.SaveChanges();
                    retorno.Add(t);
                }
            }

            return retorno;
        }

    }
}
