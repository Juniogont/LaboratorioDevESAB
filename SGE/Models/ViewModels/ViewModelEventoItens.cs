using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelEventoItens
    {
        public string Instrumento { get; set; }
        public IEnumerable<SGE.Models.EventoItem> Itens { get; set; }
        public string Subtotal { get; set; }
    }
}
