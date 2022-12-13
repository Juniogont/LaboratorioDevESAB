using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SGE.Models
{
    public class Orcamento : EntidadeGenerica
    {
        public ModeloOrcamento ModeloOrcamento { get; set; }
        [DisplayName("Modelo do Orçamento")]
        public int? ModeloOrcamentoId { get; set; }
        [DisplayName("Nº da Proposta")]
        [StringLength(10)]
        public string NumeroProposta { get; set; }
        [DisplayName("Nome do Evento")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100)]
        public string NomeEvento { get; set; }
        [DisplayName("Local do Evento")]
        public LocalEvento LocalEvento { get; set; }
        [DisplayName("Local do Evento")]
        public int? LocalEventoId { get; set; }
        [DisplayName("Situação")]
        public SituacaoOrcamento SituacaoOrcamento { get; set; }
        [DisplayName("Data da Proposta")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime dataProposta { get; set; }
        [DisplayName("Data Início")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime dataInicio { get; set; }
        [DisplayName("Data Término")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime dataFim { get; set; }
        [DisplayName("Data da Montagem")]
        public DateTime? dataMontagem { get; set; }
        [DisplayName("Tipo da Proposta")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public TipoProposta TipoProposta { get; set; }
        public Entidade Entidade { get; set; }
        [DisplayName("Cliente")]
        public int? EntidadeId { get; set; }
        [DisplayName("Solicitante")]
        [StringLength(200)]
        public string NomeSolicitante { get; set; }
        [DisplayName("Empresa Solicitante")]
        [StringLength(200)]
        public string EmpresaSolicitante { get; set; }
        [StringLength(20)]
        public string Telefone { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [DisplayName("UF")]
        public Estado Estado { get; set; }
        [DisplayName("UF")]
        public int? EstadoId { get; set; }
        public Cidade Cidade { get; set; }
        [DisplayName("Cidade")]
        public int? CidadeId { get; set; }
        [DisplayName("Mostrar Valores Unitários")]
        public bool MostrarValoresUnitarios { get; set; }
        [DisplayName("Mostrar Valor Total")]
        public bool MostrarValorTotal { get; set; }
        [DisplayName("Valor Total")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal ValorTotal { get; set; }
        [DisplayName("Valor Desconto")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal? ValorDesconto { get; set; }
        [DisplayName("Condições de Pagamento")]
        [StringLength(1000)]
        public string CondicoesPagamento { get; set; }
        [StringLength(200)]
        public string Entrega { get; set; }
        [DisplayName("Validade da Proposta")]
        [StringLength(1000)]
        public string ValidadeProposta { get; set; }
        [DisplayName("Observações")]
        [StringLength(500)]
        public string Observacoes { get; set; }
        [DisplayName("Observações do Check-List (Não aparece na proposta)")]
        [StringLength(500)]
        public string ObservacoesChecklist { get; set; }
        public bool GeradoEvento { get; set; }
    }
}
