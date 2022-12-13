using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Usuario : EntidadeGenerica
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("E-mail")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("Nome de Usuario")]
        [StringLength(10)]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(10)]
        public string Senha { get; set; }
    }
}