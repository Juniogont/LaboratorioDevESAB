using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SGE.Models.ViewModels
{
    public class ViewModelOrcamentoImprimir
    {
        public string Logomarca { get; set; }
        public SGE.Models.Orcamento Orcamento { get; set; }

        public IEnumerable<ViewModelOrcamentoItens> Intrumentos { get; set; }
    }
}
