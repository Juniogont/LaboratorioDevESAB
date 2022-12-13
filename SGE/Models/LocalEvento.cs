using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class LocalEvento : EntidadeGenerica
    {
        [StringLength(100)]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        [StringLength(500)]
        public string Descricao { get; set; }
        [StringLength(1000)]
        [DisplayName("Observações")]
        public string Observacoes { get; set; }
        [StringLength(100)]
        public string Contato { get; set; }
        [StringLength(100)]
        [DisplayName("Ponto de Referencia")]
        public string PontoReferencia { get; set; }
        public int Capacidade { get; set; }
        [StringLength(10)]
        public string CEP { get; set; }
        [StringLength(100)]
        public string Logradouro { get; set; }
        [StringLength(10)]
        [DisplayName("Número")]
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
        [DisplayName("País")]
        public int? PaisId { get; set; }
        [StringLength(20)]
        [DisplayName("Telefone")]
        public string Telefone1 { get; set; }
        [DisplayName("Telefone 2")]
        [StringLength(20)]
        public string Telefone2 { get; set; }
        [StringLength(20)]
        public string Celular { get; set; }
        [StringLength(200)]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [StringLength(200)]
        public string WebSite { get; set; }
    }
}
