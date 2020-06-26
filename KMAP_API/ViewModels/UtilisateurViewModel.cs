using KMAP_API.Models;
using System;
using System.Collections.Generic;

namespace KMAP_API.ViewModels
{
    public class UtilisateurViewModel
    {
        public Guid Id { get; set; }

        public Guid IdEntreprise { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Permis { get; set; }

        public string NomRole { get; set; }

        public string NomSite { get; set; }

        public ICollection<Personnel_Reservation> Personnel_Reservations { get; set; }

        public UtilisateurViewModel(Utilisateur u)
        {
            Id = u.Id;
            IdEntreprise = u.Site.Entreprise.Id;
            Nom = u.Nom;
            Prenom = u.Prenom;
            Mail = u.Mail;
            Permis = u.Permis;
            NomRole = u.Role.Libelle;
            NomSite = u.Site.Libelle;
            Personnel_Reservations = u.Personnel_Reservations;
        }
    }
}
