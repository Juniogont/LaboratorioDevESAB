using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Equipamento : EntidadeGenerica
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [StringLength(3000)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Fornecedor")]
        public Entidade Fornecedor { get; set; }
        [DisplayName("Fornecedor")]
        public int FornecedorId { get; set; }
        [DisplayName("Case")]
        public Case Case { get; set; }
        [DisplayName("Case")]
        public int? CaseId { get; set; }
        public Instrumento Instrumento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("Instrumento")]
        public int InstrumentoId { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; }
        [StringLength(500)]
        public string Imagem { get; set; }
        [StringLength(50)]
        [DisplayName("Número de Série")]
        public string NumeroSerie { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("Qtde em Estoque")]
        public int QuantidadeEstoque { get; set; }
        [DisplayName("Valor do Sistema")]
        public decimal? ValorSistema { get; set; }
        [DisplayName("Valor da Locação")]
        public decimal? ValorLocacao { get; set; }
        [DisplayName("Data da Aquisição")]
        public DateTime? DataAquisição { get; set; }
        [DisplayName("Data da Baixa")]
        public DateTime? DataBaixa { get; set; }
      
    }
}
