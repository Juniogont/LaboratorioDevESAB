using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class DespesaViagem : EntidadeGenerica
    {
        [DisplayName("Almoço")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Almoco { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Jantar { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Lanche { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Estadia { get; set; }
        [DisplayName("Coeficiente KM")]
        public Decimal CoeficienteKM { get; set; }
        [DisplayName("Coeficiente NF")]
        public Decimal CoeficienteNF { get; set; }
    }
}
