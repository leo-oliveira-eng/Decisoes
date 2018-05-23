using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models
{
    public class SalvarCriterioRQ
    {
        public int IdProjeto { get; set; }
        public string Nome { get; set; }
        public int TipoDeCriterio { get; set; }
        public decimal Peso { get; set; }
    }
}
