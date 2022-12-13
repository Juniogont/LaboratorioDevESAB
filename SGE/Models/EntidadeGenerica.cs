using System;
using System.ComponentModel;

namespace SGE.Models
{
    public class EntidadeGenerica
    {
        public int Id { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        public int? UsuarioCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public Status Status { get; set; }//Ativo / Inativo
    }
}
