using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Sistema : EntidadeGenerica
    {      
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [StringLength(3000)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        public Entidade Fornecedor { get; set; }
        [DisplayName("Fornecedor")]
        public int? FornecedorId { get; set; }        
        public Instrumento Instrumento { get; set; }
        public int? InstrumentoId { get; set; }
        [StringLength(10)]  
        public string Codigo { get; set; }
        [StringLength(500)]
        public string Imagem { get; set; }
        [StringLength(50)]
        [DisplayName("Número de Série")]
        public string NumeroSerie { get; set; }
        [DisplayName("Qtde em Estoque")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int QuantidadeEstoque { get; set; }
        [DisplayName("Valor do Sistema")]
        public decimal? ValorSistema { get; set; }
        [DisplayName("Valor da Locação")]
        public decimal? ValorLocacao { get; set; }
        [DisplayName("Data da Aquisição")]
        public DateTime? DataAquisição { get; set; }
        [DisplayName("Data da Baixa")]
        public DateTime? DataBaixa { get; set; }
        public ICollection<SistemaCases> Cases { get; set; }
    }
}


