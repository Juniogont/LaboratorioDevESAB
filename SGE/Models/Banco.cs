using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Banco
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(5)]
        public string Codigo { get; set; }
    }
}
