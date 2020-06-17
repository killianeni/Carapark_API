using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class ReservationUtilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdUtilisateur { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public Guid IdReservation { get; set; }

        public Reservation Reservation { get; set; }

        public ReservationUtilisateur()
        {

        }
    }
}
