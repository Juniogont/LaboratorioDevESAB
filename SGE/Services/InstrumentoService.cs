using SGE.Data;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Services
{
    public class InstrumentoService
    {
        private readonly SGEMvcContext _context;

        public InstrumentoService(SGEMvcContext context)
        {
            _context = context;
        }

        public List<Instrumento> FindAll()
        {
            return _context.Instrumento.OrderBy(x => x.Nome).ToList();
        }
    }
}
