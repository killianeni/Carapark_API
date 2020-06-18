using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Vehicule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string NumImmat { get; set; }

        public string Modele { get; set; }

        public int NbPlaces { get; set; }

        public int NbPortes { get; set; }

        public string TypeCarbu { get; set; }

        public bool Actif { get; set; }

        public Site Site { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Cle> Cles { get; set; }

        public Vehicule()
        {

        }
    }
}
