using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class CLE
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public VEHICULE Vehicule { get; set; }
    }
}
