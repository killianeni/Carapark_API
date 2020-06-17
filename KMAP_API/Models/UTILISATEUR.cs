using System.Collections.Generic;

namespace KMAP_API.Models
{
    public class Utilisateur : Personnel
    {
        public string Password { get; set; }

        public Role Role { get; set; }

        public ICollection<ReservationUtilisateur> ReservationUtilisateurs { get; set; }

        public Utilisateur() : base()
        {

        }
    }
}
