using System.Collections.Generic;

namespace SGE.Models.ViewModels
{
    public class ViewModelEquipamento
    {
        public X.PagedList.IPagedList<SGE.Models.Equipamento> Equipamentos { get; set; }
        //public IEnumerable<SGE.Models.Equipamento> Equipamentos { get; set; }
        public SGE.Models.Equipamento Equipamento { get; set; }
        public string buscaNome { get; set; }
    }
}
