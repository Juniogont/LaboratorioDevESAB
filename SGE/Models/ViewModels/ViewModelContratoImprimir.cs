using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelContratoImprimir
    {
        public SGE.Models.Contrato Contrato { get; set; }
        public IEnumerable<ViewModelEventoItens> Intrumentos { get; set; }
    }
}
