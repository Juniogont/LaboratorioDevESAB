using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models.ViewModels
{
    public class ViewModelDepesasEvento
    {
        public DespesaEvento DespesaEvento { get; set; }
        public IEnumerable<DespesaEvento> DespesasEvento { get; set; }
    }
}
