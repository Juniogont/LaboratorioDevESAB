using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Instrumento : EntidadeGenerica
    {       
        [StringLength(100)]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        [StringLength(200)]
        public string Descricao { get; set; }

        public Instrumento() { }
        public Instrumento(string nome) {
            Nome = nome;
        }
    }
}
