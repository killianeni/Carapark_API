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

        // GET: api/Reservations/GetReservationsbyEntreprise
        [Route("GetReservationsbyEntreprise/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetReservationsbyEntreprise(Guid id)
        {
            var r = new List<ReservationViewModel>();
            foreach (var reservation in await _context.Reservation
                .Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel)
                .Include(r => r.Utilisateur)
                .Include(r => r.Vehicule).ThenInclude(r => r.Site).ThenInclude(r => r.Entreprise).Where(r => r.Vehicule.Site.Entreprise.Id == id)
                .Include(r => r.Cle).ToListAsync())
            {
                r.Add(new ReservationViewModel(reservation));
            }
            return r;
        }

        // GET: api/Reservations/GetReservationsbyUser
        [Route("GetReservationsbyUser/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetReservationsbyUser(Guid id)
        {
            var r = new List<ReservationViewModel>();
            foreach (var reservation in await _context.Reservation
                .Include(r => r.Personnel_Reservations).ThenInclude(pr => pr.Personnel)
                .Include(r => r.Utilisateur).Where(u => u.Id == id)
                .Include(r => r.Vehicule).ThenInclude(r => r.Site).ThenInclude(r => r.Entreprise)
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
        public async Task<IActionResult> PutReservation(Guid id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

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

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
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

            return reservation;
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
