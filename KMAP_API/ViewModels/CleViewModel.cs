using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class CleViewModel
    {
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public string NumImmat { get; set; }

        public CleViewModel(Cle c)
        {
            Id = c.Id;
            Libelle = c.Libelle;
            NumImmat = c.Vehicule.NumImmat;
        }
    }
}
