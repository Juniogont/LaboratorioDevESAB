using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public class Contrato : EntidadeGenerica
    {
        public ModeloContrato ModeloContrato { get; set; }
        [DisplayName("Modelo do Orçamento")]
        public int? ModeloContratoId { get; set; }
        public Evento Evento { get; set; }
        [DisplayName("Evento")]
        public int EventoId { get; set; }
        public Entidade Entidade { get; set; }
        [DisplayName("Cliente")]
        public int? EntidadeId { get; set; }
        [DisplayName("Nº do Contrato")]
        [StringLength(10)]
        public string Numero { get; set; }
    }
}
