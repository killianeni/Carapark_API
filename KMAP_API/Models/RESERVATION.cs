using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Reservation
    {
        //Propriété principale
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string SiteDestination { get; set; }

        public bool ConfirmationCle { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public DateTime HeureDebut { get; set; }

        public DateTime Heurefin { get; set; }

        public string Description { get; set; }

        //Clé étrangère
        public Utilisateur Utilisateur { get; set; }

        public Vehicule Vehicule { get; set; }

        public Cle Cle { get; set; }

        //Collection pour OneToMany
        public ICollection<Personnel> Personnels { get; set; }

        public ICollection<ReservationUtilisateur> ReservationUtilisateurs { get; set; }

        public Reservation()
        {

        }
    }
}
