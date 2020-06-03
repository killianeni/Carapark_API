using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class ReservationUtilisateur
    {
        public Guid IdUtilisateur { get; set; }
        public UTILISATEUR Utilisateur { get; set; }
        public Guid IdReservation { get; set; }
        public RESERVATION Reservation { get; set; }
    }
}
