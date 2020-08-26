using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; } = new Guid();

        public DateTime DateDebut { get; set; }

        public string TimeStart { get
            {
                if (DateDebut.Hour < 13 )
                {
                    return "AM";
                }
                else
                {
                    return "PM";
                }
            } 
        }

        public DateTime DateFin { get; set; }

        public string TimeEnd
        {
            get
            {
                if (DateFin.Hour < 13)
                {
                    return "AM";
                }
                else
                {
                    return "PM";
                }
            }
        }

        public UtilisateurViewModel Utilisateur { get; set; }

        public VehiculeViewModel Vehicule { get; set; }

        public CleViewModel Cle { get; set; }

        public List<PersonnelViewModel> Personnels { get; set; } = new List<PersonnelViewModel>();

        public string SiteDestination { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public ReservationViewModel(Reservation r)
        {
            Id = r.Id;
            DateDebut = r.DateDebut;
            DateFin = r.DateFin;
            Utilisateur = new UtilisateurViewModel(r.Utilisateur);
            Vehicule = new VehiculeViewModel(r.Vehicule);
            Cle = new CleViewModel(r.Cle);

            foreach (var personnel in r.Personnel_Reservations.Select(pr => pr.Personnel))
            {
                Personnels.Add(new PersonnelViewModel(personnel));
            }
            
            SiteDestination = r.SiteDestination;
            Description = r.Description;
            Status = r.State.ToString();
        }
    }
}
