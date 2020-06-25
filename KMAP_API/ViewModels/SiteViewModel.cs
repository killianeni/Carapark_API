using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class SiteViewModel
    {
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public string NomEntreprise { get; set; }

        public ICollection<Personnel> Personnels { get; set; }

        public ICollection<Vehicule> Vehicules { get; set; }

        public SiteViewModel(Site s)
        {
            Id = s.Id;
            Libelle = s.Libelle;
            NomEntreprise = s.Entreprise.Libelle;
            Personnels = s.Personnels;
            Vehicules = s.Vehicules;
        }
    }
}
