using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class TabelaPreco : EntidadeGenerica
    {
        public short Tabela { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        public Sistema Sistema { get; set; }
        [DisplayName("Sistema")]
        public int? SistemaId { get; set; }
        public Case Case { get; set; }
        [DisplayName("Case")]
        public int? CaseId { get; set; }
        public Equipamento Equipamento { get; set; }
        [DisplayName("Equipamento")]
        public int? EquipamentoId { get; set; }
        [DisplayName("Valor de Compra")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? ValorCompra { get; set; }
        [DisplayName("Valor da Locação")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? ValorLocacao { get; set; }

    }
}
