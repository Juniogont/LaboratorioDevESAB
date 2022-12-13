using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(2)]
        public string Sigla { get; set; }
    }
}
