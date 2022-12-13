using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Helpers
{
    public class Utils
    {
        public int QuantidadeDias(DateTime dataIni, DateTime dataFim)
        {
            return (int)dataFim.Subtract(dataIni).TotalDays;
        }
    }
}
