using KMAP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Reservation
    {
        //Propriété principale
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public string SiteDestination { get; set; }

        public string Description { get; set; }

        public bool ConfirmationCle { get; set; } = false;

        public bool IsRejeted { get; set; } = false;

        public bool IsAccepted { get; set; } = false;

        public State State
        {
            get
            {
                if (ConfirmationCle)
                {
                    return State.Close;
                }
                else if (IsRejeted)
                {
                    return State.Rejet;
                }
                else if (IsAccepted)
                {
                    return State.Valid;
                }
                else
                {
                    return State.Waiting;
                }
            }
        }

        public string Commentaire { get; set; }

        //Clé étrangère
        public Utilisateur Utilisateur { get; set; }

        public Vehicule Vehicule { get; set; }

        public Cle Cle { get; set; }

        public ICollection<Personnel_Reservation> Personnel_Reservations { get; set; }

        public Reservation()
        {

        }

        public Reservation(ReservationViewModel rvm, Utilisateur u, Vehicule v, List<Personnel_Reservation> pr)
        {
            Id = rvm.Id;
            var hDebut = (rvm.TimeStart == "AM") ? 9 : 15;
            var hFin = (rvm.TimeEnd == "AM") ? 9 : 15;
            DateDebut = new DateTime(rvm.DateDebut.Year, rvm.DateDebut.Month, rvm.DateDebut.Day, hDebut, 0, 0);
            DateFin = new DateTime(rvm.DateFin.Year, rvm.DateFin.Month, rvm.DateFin.Day, hFin, 0, 0);
            SiteDestination = rvm.SiteDestination;
            Description = rvm.Description;
            ConfirmationCle = rvm.ConfirmationCle;
            IsAccepted = rvm.IsAccepted;
            IsRejeted = rvm.IsRejeted;
            Commentaire = rvm.Commentaire;
            Utilisateur = u;
            Vehicule = v;
            Personnel_Reservations = pr;
        }

        public void Update(ReservationViewModel rvm)
        {
            if(rvm.DateDebut != DateTime.MinValue)
            {
                var hDebut = (rvm.TimeStart != null) ? ((rvm.TimeStart == "AM") ? 9 : 15) : 0;
                DateDebut = (hDebut != 0) ? new DateTime(rvm.DateDebut.Year, rvm.DateDebut.Month, rvm.DateDebut.Day, hDebut, 0, 0) : DateDebut;
            }
            if (rvm.DateFin != DateTime.MinValue)
            {
                var hFin = (rvm.TimeEnd != null) ? ((rvm.TimeEnd == "AM") ? 9 : 15) : 0;
                DateFin = (hFin != 0) ? new DateTime(rvm.DateFin.Year, rvm.DateFin.Month, rvm.DateFin.Day, hFin, 0, 0) : DateFin;
            }
            SiteDestination = rvm.SiteDestination ?? SiteDestination;
            Description = rvm.Description ?? Description;
            ConfirmationCle = rvm.ConfirmationCle;
            IsAccepted = rvm.IsAccepted;
            IsRejeted = rvm.IsRejeted;
            Commentaire = rvm.Commentaire ?? Commentaire;
        }

    }


    public enum State
    {
        //En attente de validation
        Waiting = 1,

        //Validé
        Valid = 2,

        //Rejeté
        Rejet = 3,

        //Clôturé
        Close = 4
    }
}
