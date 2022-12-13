using System.Collections.Generic;

namespace SGE.Models.ViewModels
{
    public class ViewModelOrcamento
    {
        public X.PagedList.IPagedList<SGE.Models.Orcamento> Orcamentos { get; set; }
        //public IEnumerable<SGE.Models.Orcamento> Orcamentos { get; set; }
        public SGE.Models.Orcamento Orcamento { get; set; }
        public string buscaNome { get; set; }
    }
}
