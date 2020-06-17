using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class SITE
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }

        public ICollection<PERSONNEL> Personnels { get; set; }
        public ICollection<VEHICULE> Vehicules { get; set; }
    }
}
