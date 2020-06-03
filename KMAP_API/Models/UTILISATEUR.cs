using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class UTILISATEUR : PERSONNEL
    {
        public string Password { get; set; }

        public ROLE Role { get; set; }

        public ICollection<ReservationUtilisateur> ReservationUtilisateurs { get; set; }
    }
}
