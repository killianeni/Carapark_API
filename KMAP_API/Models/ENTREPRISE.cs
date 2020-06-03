using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class ENTREPRISE
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }

        public ICollection<SITE> Sites { get; set; }
    }
}
