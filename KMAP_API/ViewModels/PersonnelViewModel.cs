using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class PersonnelViewModel
    {
        public Guid Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public PersonnelViewModel(Personnel p)
        {
            Id = p.Id;
            Nom = p.Nom;
            Prenom = p.Prenom;
            Mail = p.Mail;
        }
    }
}
