using KMAP_API.Data;
using KMAP_API.Models;
using KMAP_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculesController : ControllerBase
    {
        private readonly KmapContext _context;

        public VehiculesController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Vehicules/GetVehiculesBySite
        [HttpGet("GetVehiculesBySite/{id}")]
        public async Task<ActionResult<IEnumerable<VehiculeViewModel>>> GetVehiculesBySite(Guid id)
        {
            var v = new List<VehiculeViewModel>();
            foreach (var vehicule in await _context.Vehicule.Where(v => v.Site.Id == id).Include(v => v.Cles).ToListAsync())
            {
                v.Add(new VehiculeViewModel(vehicule));
            }
            return v;
        }


        // GET: api/Vehicules/CountVehiculeActifBySite
        [HttpGet("CountVehiculeActifBySite/{id}")]
        public int CountVehiculeActifBySite(Guid id)
        {
            return _context.Vehicule.Where(v => v.Site.Id == id && v.Actif == true).Count();
        }

        // GET: api/Vehicules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculeViewModel>> GetVehicule(Guid id)
        {
            var vehicule = new VehiculeViewModel(await _context.Vehicule.Include(v => v.Cles).FirstOrDefaultAsync(v => v.Id == id));

            if (vehicule == null)
            {
                return NotFound();
            }

            return vehicule;
        }

        // PUT: api/Vehicules/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicule(Guid id, Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculeExists(id))
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

        // POST: api/Vehicules
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vehicule>> PostVehicule(Vehicule vehicule)
        {
            _context.Vehicule.Add(vehicule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicule", new { id = vehicule.Id }, vehicule);
        }

        // DELETE: api/Vehicules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicule>> DeleteVehicule(Guid id)
        {
            var vehicule = await _context.Vehicule.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }

            _context.Vehicule.Remove(vehicule);
            await _context.SaveChangesAsync();

            return vehicule;
        }

        private bool VehiculeExists(Guid id)
        {
            return _context.Vehicule.Any(e => e.Id == id);
        }
    }
}
