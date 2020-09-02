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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
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
            if (id != reservationVM.Id)
            {
                return BadRequest();
            }
            var reservation = _context.Reservation.Where(r => r.Id == reservationVM.Id).FirstOrDefault();
            Vehicule v = _context.Vehicule.Where(v => v.Id == reservationVM.Vehicule.Id).FirstOrDefault();
            List<Personnel_Reservation> pr = new List<Personnel_Reservation>();
            foreach (var p in reservationVM.Personnels)
            {
                pr.Add(new Personnel_Reservation()
                {
                    PersonnelId = p.Id,
                    ReservationID = reservationVM.Id
                });
            }
            reservation.Update(reservationVM, v, pr);

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
        public async Task<ActionResult<Reservation>> PostReservation(ReservationViewModel reservationVM)
        {
            Utilisateur u = _context.Utilisateur.Where(u => u.Id == reservationVM.Utilisateur.Id).FirstOrDefault();
            Vehicule v = _context.Vehicule.Where(v => v.Id == reservationVM.Vehicule.Id).FirstOrDefault();
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
        public async Task<ActionResult<Reservation>> DeleteReservation(Guid id)
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

        // GET: api/Reservations/GetFullReservedDays
        /// <param name="date"> mm-aaaa (si 01 -> 1 ) </param>
        [Route("GetFullReservedDays/{idSite}/{date}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetFullReservedDays(Guid idSite, string date)
        {
            var fullDays = new HashSet<DateTime>();
            var nbMaxVehicule = new VehiculesController(_context).CountVehiculeActifBySite(idSite);

            var listResa = (await GetReservationsBySiteAndDate(idSite, date)).Value.Where(r => (State)r.Status != State.Rejet);
            var year = Int32.Parse(date.Split('-')[1]);
            var month = Int32.Parse(date.Split('-')[0]);
            var dateT = new DateTime(year, month, 1, 9, 0, 0);

            for (int i = 0; i < 31; i++)
            {
                if (listResa.Where(r => r.DateDebut <= dateT && r.DateFin >= dateT).Count() == nbMaxVehicule)
                {
                    fullDays.Add(dateT);
                }
                dateT = dateT.AddHours(6);
                if (listResa.Where(r => r.DateDebut <= dateT && r.DateFin >= dateT).Count() == nbMaxVehicule)
                {
                    fullDays.Add(dateT);
                }
                dateT = dateT.AddHours(18);
            }

            return fullDays;
        }


        #region Function private

        //Get true if reservation exist
        private bool ReservationExists(Guid id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }


        #endregion
    }
}
