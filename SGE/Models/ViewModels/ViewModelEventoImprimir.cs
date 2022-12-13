using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelEventoImprimir
    {
        public SGE.Models.Evento Evento { get; set; }
        public IEnumerable<DespesaEvento> DespesasEvento { get; set; }
        public IEnumerable<ViewModelEventoItens> Intrumentos { get; set; }
    }
}
