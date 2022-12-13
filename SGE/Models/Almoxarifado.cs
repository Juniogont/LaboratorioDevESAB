using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Almoxarifado : EntidadeGenerica
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [StringLength(3000)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Qtde em Estoque")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int QuantidadeEstoque { get; set; }
        [DisplayName("Local")]
        public LocalEvento LocalEvento { get; set; }
        [DisplayName("Local")]
        public int? LocalEventoId { get; set; }
        [DisplayName("Posição")]
        public PosicaoAlmoxarifado PosicaoAlmoxarifado { get; set; }
        [DisplayName("Posição")]
        public int? PosicaoAlmoxarifadoId { get; set; }

    }
}
