using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class ModeloContrato : EntidadeGenerica
    {
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(500)]
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [StringLength(1000)]
        [DisplayName("Texto Inicial")]
        public string TextoInicial { get; set; }
        [StringLength(1000)]
        [DisplayName("Texto Pós Contratante")]
        public string TextoInicialPosContratante { get; set; }
        [StringLength(4000)]
        [DisplayName("Clausula Primeira")]
        public string ClausulaPrimeira { get; set; }
        [StringLength(4000)]
        [DisplayName("Clausula Segunda")]
        public string ClausulaSegunda { get; set; }
        [StringLength(4000)]
        [DisplayName("Clausula Terceira")]
        public string ClausulaTerceira { get; set; }
        [StringLength(4000)]
        [DisplayName("Clausula Quarta")]
        public string ClausulaQuarta { get; set; }
        [StringLength(4000)]
        [DisplayName("Clausula Quinta")]
        public string ClausulaQuinta { get; set; }
        [StringLength(10000)]
        [DisplayName("Clausulas Finais")]
        public string ClausulasFinais { get; set; }

    }
}
