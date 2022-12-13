using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class PlanoContas : EntidadeGenerica
    {
        public TipoMovimentacao TipoMovimentacao { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
    }
}
