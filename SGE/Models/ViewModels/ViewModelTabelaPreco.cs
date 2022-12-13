using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelTabelaPreco
    {
        public IEnumerable<SGE.Models.TabelaPreco> Precos { get; set; }
        public string buscaNome { get; set; }
        public short TipoPreco { get; set; }
        public short Tabela { get; set; }
        public decimal? Percentual { get; set; }

    }
}
