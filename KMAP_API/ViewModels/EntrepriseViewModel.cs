using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class EntrepriseViewModel
    {
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public ICollection<Site> Sites { get; set; }

        public EntrepriseViewModel(Entreprise e)
        {
            Id = e.Id;
            Libelle = e.Libelle;
            Sites = e.Sites;
        }
    }
}
