using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelEntidade
    {
        public X.PagedList.IPagedList<SGE.Models.Entidade> Entidades { get; set; }
        //public IEnumerable<SGE.Models.Entidade> Entidades { get; set; }
        public SGE.Models.Entidade Entidade { get; set; }
        public string buscaNome { get; set; }

        public TipoEntidade? TipoEntidade { get; set; }
    }
}
