using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelEvento
    {
        public IEnumerable<SGE.Models.Evento> Eventos { get; set; }
        public SGE.Models.Evento Evento { get; set; }
        public string buscaNome { get; set; }
    }
}
