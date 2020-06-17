using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class ROLE
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }

        public ICollection<UTILISATEUR> Utilisateurs { get; set; }
    }
}
