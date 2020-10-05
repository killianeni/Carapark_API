using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMAP_API.Models;

namespace KMAP_API.ViewModels
{
    public class Personnel_ReservationViewModel
    {
        public Guid PersonnelId { get; set; }

        public Guid ReservationID { get; set; }

        public Personnel_ReservationViewModel(Personnel_Reservation pr)
        {
            PersonnelId = pr.PersonnelId;
            ReservationID = pr.ReservationID;
        }


    }
}
