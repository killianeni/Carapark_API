using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class RESERVATION
    {
        //Propriété principale
        public Guid Id { get; set; }
        public string SiteDestination { get; set; }
        public bool ConfirmationCle { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime Heurefin { get; set; }
        public string Description { get; set; }

        //Clé étrangère
        public UTILISATEUR Utilisateur { get; set; }
        public VEHICULE Vehicule { get; set; }
        public CLE Cle { get; set; }

        //Collection pour OneToMany
        public ICollection<PERSONNEL> Personnels { get; set; }
        public ICollection<ReservationUtilisateur> ReservationUtilisateurs { get; set; }
    }
}
