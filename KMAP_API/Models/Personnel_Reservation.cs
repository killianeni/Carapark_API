using System;

namespace KMAP_API.Models
{
    public class Personnel_Reservation
    {
        public Guid PersonnelId { get; set; }

        public Personnel Personnel { get; set; }

        public Guid ReservationID { get; set; }

        public Reservation Reservation { get; set; }

        public Personnel_Reservation()
        {

        }
    }
}
