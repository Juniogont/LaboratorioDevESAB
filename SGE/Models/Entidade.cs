using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Entidade : EntidadeGenerica
    {       
        [StringLength(100)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("Razão Social")]
        public string Nome { get; set; }
        [StringLength(100)]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("Tipo Pessoa")]
        public TipoPessoa TipoPessoa { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DisplayName("CPF/CNPJ")]
        public string CPFCNPJ { get; set; }
        [StringLength(30)]
        [DisplayName("Inscricao Estadual")]
        public string RGInscricaoEstadual { get; set; }
        [StringLength(10)]
        public string CEP { get; set; }
        [StringLength(100)]
        public string Logradouro { get; set; }
        [StringLength(10)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Complemento { get; set; }
        [StringLength(50)]
        public string Bairro { get; set; }
        public Cidade Cidade { get; set; }
        [DisplayName("Cidade")]
        public int? CidadeId { get; set; }
        public Estado Estado { get; set; }
        [DisplayName("Estado")]
        public int? EstadoId { get; set; }
        [StringLength(50)]
        public Pais Pais { get; set; }
        public int? PaisId { get; set; }
        [StringLength(20)]
        public string Telefone1 { get; set; }
        [StringLength(20)]
        public string Telefone2 { get; set; }
        [StringLength(20)]
        public string Celular { get; set; }
        [StringLength(200)]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [StringLength(200)]        
        public string WebSite { get; set; }
        [StringLength(500)]
        [DisplayName("Observações")]
        public string Observacoes { get; set; }
        public bool Cliente { get; set; }
        public bool Fornecedor { get; set; }
       
    }
}
