using System.Collections.Generic;

namespace Site.Models
{
    public class SalvarAlternativasRQ
    {
        public int Id { get; set; }
        public List<string> Alternativas { get; set; }
    }
}