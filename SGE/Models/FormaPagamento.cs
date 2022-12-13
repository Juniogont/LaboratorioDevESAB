using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class FormaPagamento :EntidadeGenerica
    {
        [StringLength(100)]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        [StringLength(500)]
        public string Descricao { get; set; }
        [DisplayName("Meio de Pagamento")]
        public MeioPagamento MeioPagamento { get; set; }
        [DisplayName("Quantidade de Parcelas")]
        public short QuantidadeParcelas { get; set; }
    }
}
