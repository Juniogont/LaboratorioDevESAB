using SGE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Areas.Identity.Data
{
    public class EmpresaUsuario
    {
        public int Id { get; set; }
        [StringLength(36)]
        public string UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
    }
}
