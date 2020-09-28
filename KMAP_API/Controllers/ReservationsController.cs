using KMAP_API.Data;
using KMAP_API.Models;
using KMAP_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "user,admin,super-admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly KmapContext _context;

        public ReservationsController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Reservations/GetReservationsBySite
        [Route("GetReservationsBySite/{idSite}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetReservationsBySite(Guid idSite)
        {
            var r = new List<ReservationViewModel>();
            foreach (var reservation in await _context.Reservation
                .Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel)
                .Include(r => r.Utilisateur).ThenInclude(u => u.Role)
                .Include(r => r.Vehicule).ThenInclude(v => v.Cles)
                .Include(r => r.Vehicule).ThenInclude(v => v.Site).ThenInclude(s => s.Entreprise).Where(r => r.Vehicule.Site.Id == idSite)
                .Include(r => r.Cle).ToListAsync())
            {
                r.Add(new ReservationViewModel(reservation));
            }
            return r;
        }

        // GET: api/Reservations/GetReservationsBySiteAndDate
        /// <param name="date"> mm-aaaa (si 01 -> 1 ) </param>
        [Route("GetReservationsBySiteAndDate/{idSite}/{date}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetReservationsBySiteAndDate(Guid idSite, string date)
        {
            var r = new List<ReservationViewModel>();
            var annee = date.Split('-')[1];
            var mois = date.Split('-')[0];

            foreach (var reservation in await _context.Reservation.Where(r => (r.DateDebut.Year.ToString() == annee && r.DateDebut.Month.ToString() == mois) || (r.DateFin.Year.ToString() == annee && r.DateFin.Month.ToString() == mois))
                .Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel)
                .Include(r => r.Utilisateur).ThenInclude(u => u.Role)
                .Include(r => r.Vehicule).ThenInclude(v => v.Site).ThenInclude(s => s.Entreprise).Where(r => r.Vehicule.Site.Id == idSite)
                .Include(r => r.Cle).ToListAsync())
            {
                r.Add(new ReservationViewModel(reservation));
            }
            return r;
        }

        // GET: api/Reservations/GetReservationsbyUser
        [Route("GetReservationsByUser/{idUser}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetReservationsByUser(Guid idUser)
        {
            var r = new List<ReservationViewModel>();
            foreach (var reservation in await _context.Reservation
                .Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel)
                .Include(r => r.Utilisateur).ThenInclude(u => u.Role).Where(u => u.Utilisateur.Id == idUser)
                .Include(r => r.Vehicule).ThenInclude(v => v.Site).ThenInclude(s => s.Entreprise)
                .Include(r => r.Cle).ToListAsync())
            {
                r.Add(new ReservationViewModel(reservation));
            }
            return r;
        }

        // GET: api/Reservations/GetFullReservedDays
        /// <param name="date"> mm-aaaa (si 01 -> 1 ) </param>
        [Route("GetFullReservedDays/{idSite}/{date}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullDay>>> GetFullReservedDays(Guid idSite, string date)
        {
            var fullDays = new List<FullDay>();
            var nbMaxVehicule = new VehiculesController(_context).CountVehiculeActifBySite(idSite);

            var listResa = (await GetReservationsBySiteAndDate(idSite, date)).Value.Where(r => (State)r.Status != State.Rejet);
            var year = Int32.Parse(date.Split('-')[1]);
            var month = Int32.Parse(date.Split('-')[0]);
            var dateT = new DateTime(year, month, 1, 9, 0, 0);

            FullDay fullDay;

            for (int i = 0; i < 31; i++)
            {
                fullDay = new FullDay() { Date = dateT };
                if (listResa.Where(r => r.DateDebut <= dateT && r.DateFin >= dateT).Count() == nbMaxVehicule)
                {
                    fullDay.AM = true;
                }
                dateT = dateT.AddHours(6);
                if (listResa.Where(r => r.DateDebut <= dateT && r.DateFin >= dateT).Count() == nbMaxVehicule)
                {
                    fullDay.PM = true;
                }
                dateT = dateT.AddHours(18);

                if (fullDay.AM || fullDay.PM)
                {
                    fullDays.Add(fullDay);
                }
            }

            return fullDays;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationViewModel>> GetReservation(Guid id)
        {
            var reservation = new ReservationViewModel(await _context.Reservation.Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel).Include(r => r.Utilisateur).Include(r => r.Vehicule).Include(r => r.Cle).FirstOrDefaultAsync(r => r.Id == id));

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, ReservationViewModel reservationVM)
        {
            if (!_context.Reservation.Any(r => r.Id == id))
            {
                return BadRequest();
            }

            var reservation = _context.Reservation.Include(r => r.Utilisateur).Include(r => r.Personnel_Reservations).FirstOrDefault(r => r.Id == id);

            CreateNotifications(reservation, reservationVM);

            if (reservationVM.Vehicule != null && reservationVM.Vehicule.Id != null)
            {
                var vehicule = _context.Vehicule.FirstOrDefault(v => v.Id == reservationVM.Vehicule.Id);
                reservation.Vehicule = vehicule;
            }

            if (reservationVM.Cle != null && reservationVM.Cle.Id != null)
            {
                var cle = _context.Cle.FirstOrDefault(c => c.Id == reservationVM.Cle.Id);
                reservation.Cle = cle;
            }

            List<Personnel_Reservation> pr = new List<Personnel_Reservation>();
            var pIds = reservationVM.Personnels.Select(p => p.Id).ToList();
            foreach (var p in reservation.Personnel_Reservations)
            {
                if (pIds.Contains(p.PersonnelId))
                {
                    pIds.Remove(p.PersonnelId);
                    pr.Add(p);
                }

            }
            foreach (var pid in pIds)
            {
                pr.Add(new Personnel_Reservation()
                {
                    PersonnelId = pid,
                    ReservationID = reservationVM.Id
                });
            }
            reservation.Personnel_Reservations = pr;

            reservation.Update(reservationVM);

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostReservation(ReservationViewModel reservationVM)
        {
            Utilisateur u = _context.Utilisateur.Where(ut => ut.Id == reservationVM.Utilisateur.Id).FirstOrDefault();
            Vehicule v = _context.Vehicule.Where(vh => vh.Id == reservationVM.Vehicule.Id).FirstOrDefault();
            List<Personnel_Reservation> pr = new List<Personnel_Reservation>();

            foreach (var p in reservationVM.Personnels)
            {
                pr.Add(new Personnel_Reservation()
                {
                    PersonnelId = p.Id,
                    ReservationID = reservationVM.Id
                });
            }
            var reservation = new Reservation(reservationVM, u, v, pr);

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        #region Function private

        //Get true if reservation exist
        private bool ReservationExists(Guid id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }

        private void CreateNotifications(Reservation r, ReservationViewModel rvm)
        {
            if (!r.IsAccepted && rvm.IsAccepted)
            {
                _context.Notification.Add(new Notification()
                {
                    Utilisateur = r.Utilisateur,
                    Reservation = r,
                    Commentaire = "La réservation à été validé",
                    DateNotif = DateTime.Now,
                    TypeNotif = State.Valid
                });
            }
            if (!r.IsRejeted && rvm.IsRejeted)
            {
                _context.Notification.Add(new Notification()
                {
                    Utilisateur = r.Utilisateur,
                    Reservation = r,
                    Commentaire = rvm.Commentaire,
                    DateNotif = DateTime.Now,
                    TypeNotif = State.Rejet
                });
            }
            if (!r.ConfirmationCle && rvm.ConfirmationCle)
            {
                _context.Notification.Add(new Notification()
                {
                    Utilisateur = r.Utilisateur,
                    Reservation = r,
                    Commentaire = "La réservation à été cloturé",
                    DateNotif = DateTime.Now,
                    TypeNotif = State.Close
                });
            }
        }

        #endregion
    }
}
