using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Utilisateur Utilisateur { get; set; }

        public Reservation Reservation { get; set; }

        public DateTime DateNotif { get; set; }

        public State TypeNotif { get; set; }

        public string Commentaire { get; set; }

        public bool Checked { get; set; }

        public Notification()
        {

        }
    }
}
