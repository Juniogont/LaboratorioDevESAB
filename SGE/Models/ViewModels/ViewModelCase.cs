using System.Collections.Generic;

namespace SGE.Models.ViewModels
{
    public class ViewModelCase
    {
        public IEnumerable<SGE.Models.Case> Cases { get; set; }
        public SGE.Models.Case Case { get; set; }
        public string buscaNome { get; set; }
        public IEnumerable<SGE.Models.Equipamento> Equipamentos { get; set; }
    }
}
