using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string RazaoSocial { get; set; }
        [StringLength(100)]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [StringLength(18)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CNPJ { get; set; }
        [StringLength(36)]
        public string Token { get; set; } 
        public bool IsActive { get; set; }
        [StringLength(15)]
        public string Telefone { get; set; }
        [StringLength(15)]
        public string Celular { get; set; }
        [StringLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
