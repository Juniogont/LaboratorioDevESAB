using System.Collections.Generic;

namespace SGE.Models.ViewModels
{
    public class ViewModelSistema
    {
        public IEnumerable<SGE.Models.Sistema> Sistemas { get; set; }
        public SGE.Models.Sistema Sistema { get; set; }
        public string buscaNome { get; set; }
    }
}
