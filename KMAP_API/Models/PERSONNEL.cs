using KMAP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Personnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Permis { get; set; }

        public Site Site { get; set; }

        public ICollection<Personnel_Reservation> Personnel_Reservations { get; set; } = new List<Personnel_Reservation>();

        public Personnel()
        {

        }

        public void Update(PersonnelViewModel pvm)
        {
            Nom = pvm.Nom;
            Prenom = pvm.Prenom;
            Mail = pvm.Mail;
            Permis = pvm.Permis;
        }
    }
}
