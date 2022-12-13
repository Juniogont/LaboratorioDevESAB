using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class PosicaoAlmoxarifado : EntidadeGenerica
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [StringLength(3000)]
        [DisplayName("Observação")]
        public string Observacao { get; set; }
    }
}
