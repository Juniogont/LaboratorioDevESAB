using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class OrcamentoItem
    {
        public int Id { get; set; }        
        public Orcamento Orcamento { get; set; }
        public int OrcamentoId { get; set; }
        public Case Case { get; set; }
        public int? CaseId { get; set; }
        public Equipamento Equipamento { get; set; }
        public int? EquipamentoId { get; set; }
        public Sistema Sistema { get; set; }
        public int? SistemaId { get; set; }
        [DisplayName("Instrumento")]
        public Instrumento Instrumento { get; set; }
        [DisplayName("Instrumento")]
        public int? InstrumentoId { get; set; }
        [StringLength(100)]
        [DisplayName("Instrumento")]
        public string NomeInstrumento { get; set; }
        [StringLength(100)]
        [DisplayName("Sistema")]
        public string NomeSistema { get; set; }
        [DisplayName("Equipamentos")]
        [StringLength(1000)]
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Valor { get; set; }

        [DisplayName("Diárias")]
        public int Diarias { get; set; }
        [DisplayName("Subtotal")]
        public Decimal ValorTotalItem { get; set; }
        public Decimal ValorSublocacao { get; set; }
    }
}
