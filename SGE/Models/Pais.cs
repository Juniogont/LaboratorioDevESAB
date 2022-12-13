using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Pais
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
