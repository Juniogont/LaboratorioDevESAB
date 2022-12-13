using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Montagem : EntidadeGenerica
    {
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
