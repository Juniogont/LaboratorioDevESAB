using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class MovimentoFinanceiro : EntidadeGenerica
    {
        public FormaPagamento FormaPagamento { get; set; }
        [DisplayName("Forma de Pagamento")]
        public int FormaPagamentoId { get; set; }
        public PlanoContas PlanoContas { get; set; }
        [DisplayName("Plano de Contas")]
        public int PlanoContasId { get; set; }
        public ContaCorrente ContaCorrente { get; set; }        
        [DisplayName("Conta Corrente")]
        public int? ContaCorrenteId { get; set; }
        public Evento Evento { get; set; }
        [DisplayName("Evento")]
        public int? EventoId { get; set; }
        public Entidade Entidade { get; set; }
        public int? EntidadeId { get; set; }
        [StringLength(500)]
        [DisplayName("Nº Documento")]
        public string NumeroDocumento { get; set; }
        [StringLength(100)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [StringLength(500)]
        [DisplayName("Observações")]
        public string Observacoes { get; set; }
        [DisplayName("Data Vencimento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime dataVencimento { get; set; }
        [DisplayName("Data Pagamento")]
        [DataType(DataType.Date)]
        public DateTime? dataPagamento { get; set; }
        public bool Pago { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal? Valor { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
    }
}
