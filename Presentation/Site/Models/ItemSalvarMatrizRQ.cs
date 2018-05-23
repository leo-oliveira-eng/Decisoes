using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models
{
    public class ItemSalvarMatrizRQ
    {
        public int IdProjeto { get; set; }
        public int IdAlternativa { get; set; }
        public int IdCriterio { get; set; }
        public string Valor { get; set; }
    }
}

