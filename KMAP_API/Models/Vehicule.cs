using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    [Table("Vehicule")]
    public class Vehicule
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [ForeignKey("Site")]
        public Guid IdSite { get; set; }
        public Site Site { get; set; }

        [Column("numImmat")]
        [MaxLength(8)]
        public string NumImmat { get; set; }

        [Column("modele")]
        [MaxLength(32)]
        public string Modele { get; set; }

        [Column("nbPlaces")]
        public int NbPlaces { get; set; }

        [Column("nbPortes")]
        public int NbPortes { get; set; }

        [Column("typeCarbu")]
        [MaxLength(16)]
        public string TypeCarbu { get; set; }

        [Column("actif")]
        public bool Actif { get; set; }
    }
}
