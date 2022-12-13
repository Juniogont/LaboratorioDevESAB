using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class Funcionario : EntidadeGenerica
    {
        [StringLength(200)]
        public string Nome { get; set; }
        [StringLength(100)]
        public string Tratamento { get; set; }
        public Cargo Cargo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        [StringLength(100)]
        public string Escolaridade { get; set; }
        [DisplayName("Data de Nasc.")]
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
        [StringLength(200)]
        [DisplayName("Nome do Pai")]
        public string NomePai { get; set; }
        [StringLength(200)]
        [DisplayName("Nome da Mãe")]
        public string NomeMae { get; set; }
        [DisplayName("UF Naturalidade")]
        public Estado EstadoNasc { get; set; }
        [DisplayName("UF Naturalidade")]
        public int? EstadoNascId { get; set; }
        public Cidade CidadeNasc { get; set; }
        [DisplayName("Naturalidade")]
        public int? CidadeNascId { get; set; }
        [StringLength(10)]
        public string CEP { get; set; }
        [StringLength(100)]
        public string Logradouro { get; set; }
        [StringLength(10)]
        [DisplayName("Námero")]
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
        [DisplayName("Data de Admissão")]
        [DataType(DataType.Date)]
        public DateTime? DataAdmissao { get; set; }
        [DisplayName("Data de Demissão")]
        [DataType(DataType.Date)]
        public DateTime? DataDemissao { get; set; }
        [StringLength(20)]
        public string CPF { get; set; }
        [StringLength(20)]
        public string RG { get; set; }
        [StringLength(20)]
        [DisplayName("Orgão Emissor")]
        public string OrgaoEmissor { get; set; }
        [DisplayName("Carteira Profissional")]
        [StringLength(20)]
        public string CTPS { get; set; }
        [StringLength(20)]
        public string CNH { get; set; }
    }
}
