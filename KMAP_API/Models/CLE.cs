using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KMAP_API.Models
{
    public class Cle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        [JsonIgnore]
        public Vehicule Vehicule { get; set; }

        public Cle()
        {

        }
    }
}
