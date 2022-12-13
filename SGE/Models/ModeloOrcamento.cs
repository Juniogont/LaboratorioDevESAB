using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class ModeloOrcamento : EntidadeGenerica
    {
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(1000)]
        public string TextoInicial { get; set; }
        [StringLength(4000)]
        public string TextoFinal { get; set; }
        [StringLength(1000)]
        public string Assinatura { get; set; }
        [StringLength(1000)]
        public string Rodapé { get; set; }
        public byte[] Logomarca { get; set; }
        [StringLength(50)]
        public string ContentType { get; set; }
        [StringLength(200)]
        public string RazaoEmpresa { get; set; }
        [StringLength(20)]
        public string CNPJ { get; set; }
        [StringLength(500)]
        public string LogomarcaPath { get; set; }
    }
}
