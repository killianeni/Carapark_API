using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Models
{
    public class VEHICULE
    {
        public Guid Id { get; set; }
        public string NumImmat { get; set; }
        public string Modele { get; set; }
        public int NbPlaces { get; set; }
        public int NbPortes { get; set; }
        public string TypeCarbu { get; set; }
        public bool Actif { get; set; }

        public SITE Site { get; set; }

        public ICollection<RESERVATION> Reservations { get; set; }
        public ICollection<CLE> Cles { get; set; }
    }
}
