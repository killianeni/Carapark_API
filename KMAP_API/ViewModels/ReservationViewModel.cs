using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }

        public string SiteDestination { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public string NumImmat { get; set; }

        public string Modele { get; set; }

        public string NomCle { get; set; }

        public List<string> personnels { get; set; } = new List<string>();

        public ReservationViewModel(Reservation r)
        {
            Id = r.Id;
            SiteDestination = r.SiteDestination;
            DateDebut = r.DateDebut;
            DateFin = r.DateFin;
            Description = r.Description;
            FullName = r.Utilisateur.Nom + " " + r.Utilisateur.Prenom;
            NumImmat = r.Vehicule.NumImmat;
            Modele = r.Vehicule.Modele;
            NomCle = r.Cle.Libelle;
            foreach (var personne in r.Personnel_Reservations)
            {
                personnels.Add(personne.Personnel.Nom + " " + personne.Personnel.Prenom);
            }
        }
    }
}
