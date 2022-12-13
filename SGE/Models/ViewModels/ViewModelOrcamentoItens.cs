using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelOrcamentoItens
    {
        public string Instrumento { get; set; }
        public IEnumerable<SGE.Models.OrcamentoItem> Itens { get; set; }
        public string Subtotal { get; set; }
    }
}
