using System;
using KMAP_API.Models;

namespace KMAP_API.ViewModels
{
    public class PersonnelViewModel
    {
        public Guid Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Permis { get; set; }

        public Guid SiteId { get; set; }

        public PersonnelViewModel()
        {

        }

        public PersonnelViewModel(Personnel p)
        {
            Id = p.Id;
            Nom = p.Nom;
            Prenom = p.Prenom;
            Mail = p.Mail;
            Permis = p.Permis;
        }
    }
}
