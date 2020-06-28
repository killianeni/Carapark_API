using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KMAP_API.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        [JsonIgnore]
        public ICollection<Utilisateur> Utilisateurs { get; set; }

        public Role()
        {

        }
    }
}
