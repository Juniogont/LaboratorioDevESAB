using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class Endereco : EntidadeGenerica
    {
        public Entidade Entidade { get; set; }
        public int? EntidadeId { get; set; }
        [StringLength(50)]
        [DisplayName("Nome do Local")]
        public string Nome { get; set; }
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
        [DisplayName("Telefone")]
        public string Telefone1 { get; set; }
        [StringLength(20)]
        [DisplayName("Telefone 2")]
        public string Telefone2 { get; set; }
        [StringLength(20)]
        public string Celular { get; set; }
        [StringLength(200)]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [StringLength(200)]
        [DisplayName("Site")]
        public string WebSite { get; set; }
        [DisplayName("Endereço Principal")]
        public bool Principal { get; set; }
    }
}
