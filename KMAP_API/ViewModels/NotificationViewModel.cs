using System;
using KMAP_API.Models;

namespace KMAP_API.ViewModels
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdResa { get; set; }

        public DateTime DateResa { get; set; }

        public DateTime DateNotif { get; set; }

        public State TypeNotif { get; set; }

        public string Commentaire { get; set; }

        public bool Checked { get; set; }

        public NotificationViewModel()
        {

        }

        public NotificationViewModel(Notification n)
        {
            Id = n.Id;
            IdUser = n.Utilisateur.Id;
            IdResa = n.Reservation.Id;
            DateResa = n.Reservation.DateDebut;
            DateNotif = n.DateNotif;
            TypeNotif = n.TypeNotif;
            Commentaire = n.Commentaire;
            Checked = n.Checked;
        }
    }
}
