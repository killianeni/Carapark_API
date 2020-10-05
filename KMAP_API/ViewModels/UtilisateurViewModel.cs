using System;
using System.Collections.Generic;
using KMAP_API.Models;

namespace KMAP_API.ViewModels
{
    public class UtilisateurViewModel : PersonnelViewModel
    {
        public string Password { get; set; }

        public bool ResetPass { get; set; }

        public Guid IdEntreprise { get; set; }

        public RoleViewModel Role { get; set; }

        public SiteViewModel Site { get; set; }

        public ICollection<Personnel_ReservationViewModel> Personnel_Reservations { get; set; } = new List<Personnel_ReservationViewModel>();

        public UtilisateurViewModel() : base()
        {

        }

        public UtilisateurViewModel(Utilisateur u)
        {
            Id = u.Id;
            IdEntreprise = u.Site.Entreprise.Id;
            Nom = u.Nom;
            Prenom = u.Prenom;
            Mail = u.Mail;
            Permis = u.Permis;
            Role = new RoleViewModel()
            {
                Id = u.Role.Id,
                Libelle = u.Role.Libelle
            };
            Site = new SiteViewModel()
            {
                Id = u.Site.Id,
                Libelle = u.Site.Libelle
            };
            foreach (var pr in u.Personnel_Reservations)
            {
                Personnel_Reservations.Add(new Personnel_ReservationViewModel(pr));
            }
        }
    }
}
