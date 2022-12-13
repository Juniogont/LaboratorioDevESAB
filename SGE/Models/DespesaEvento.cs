using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class DespesaEvento : EntidadeGenerica
    {
        public Evento Evento { get; set; }
        public int EventoId { get; set; }
        public Funcionario Funcionario { get; set; }
        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int FuncionarioId { get; set; }
        public Montagem Montagem { get; set; }
        [Display(Name = "Montagem")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int MontagemId { get; set; }
        [DisplayName("Almoço")]
        public bool Almoco { get; set; }
        public bool Jantar { get; set; }
        public bool Lanche { get; set; }
        public bool Estadia { get; set; }
        [DisplayName("Almoço")]
        public Decimal ValorAlmoco { get; set; }
        [DisplayName("Jantar")]
        public Decimal ValorJantar { get; set; }
        [DisplayName("Lanche")]
        public Decimal ValorLanche { get; set; }
        [DisplayName("Estadia")]
        public Decimal ValorEstadia { get; set; }
        [DisplayName("Diversos")]
        public Decimal ValorDiversos { get; set; }
        [DisplayName("Total")]
        public Decimal ValorTotal { get; set; }
    }
}
