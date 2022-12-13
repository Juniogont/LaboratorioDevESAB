using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        public int IdEstado { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
