using KMAP_API.Models;
using System;
using System.Collections.Generic;

namespace KMAP_API.ViewModels
{
    public class SiteViewModel
    {
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public EntrepriseViewModel Entreprise { get; set; }

        public int NbUtilisateurs { get; set; }

        public ICollection<Personnel> Utilisateurs { get; set; }

        public int NbVehicules { get; set; }

        public ICollection<Vehicule> Vehicules { get; set; }

        public int NbReservations { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public SiteViewModel()
        {

        }

        public SiteViewModel(Site s)
        {
            Id = s.Id;
            Libelle = s.Libelle;
            Entreprise = new EntrepriseViewModel()
            {
                Id = s.Entreprise.Id,
                Libelle = s.Entreprise.Libelle
            };
        }
    }
}
