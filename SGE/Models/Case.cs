using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Case : EntidadeGenerica
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
        [StringLength(3000)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; }
        public Sistema Sistema { get; set; }
        [DisplayName("Sistema")]
        public int? SistemaId { get; set; }
        [DisplayName("Valor da Locação")]
        public decimal? ValorLocacao { get; set; }
        public ICollection<CaseEquipamentos> Equipamentos { get; set; }
    }
}
