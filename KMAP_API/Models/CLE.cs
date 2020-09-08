using KMAP_API.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Cle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public Vehicule Vehicule { get; set; }

        public Cle()
        {

        }

        public void Update(CleViewModel cvm)
        {
            Libelle = cvm.Libelle;
        }
    }
}
