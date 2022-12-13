using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class ContaCorrente : EntidadeGenerica
    {
        [StringLength(20)]
        [Display(Name = "Número")]
        public string numero { get; set; }
        [StringLength(30)]
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [StringLength(10)]
        [Display(Name = "Agência")]
        public string agencia { get; set; }
        public Banco Banco { get; set; }
        [Display(Name = "Banco")]
        public int BancoId { get; set; }
    }
}
