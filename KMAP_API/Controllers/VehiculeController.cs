using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KMAP_API.Data;
using KMAP_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace KMAP_API.Controllers
{
    [Authorize/*(Roles = "Admin")*/]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculeController : ControllerBase
    {
        private readonly KmapContext _context;

        public VehiculeController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Vehicule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VEHICULE>>> GetVehicule()
        {
            return await _context.Vehicule.ToListAsync();
        }

        // GET: api/Vehicule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VEHICULE>> GetVEHICULE(Guid id)
        {
            var vEHICULE = await _context.Vehicule.FindAsync(id);

            if (vEHICULE == null)
            {
                return NotFound();
            }

            return vEHICULE;
        }

        // PUT: api/Vehicule/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVEHICULE(Guid id, VEHICULE vEHICULE)
        {
            if (id != vEHICULE.Id)
            {
                return BadRequest();
            }

            _context.Entry(vEHICULE).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VEHICULEExists(id))
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

        // POST: api/Vehicule
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VEHICULE>> PostVEHICULE(VEHICULE vEHICULE)
        {
            _context.Vehicule.Add(vEHICULE);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVEHICULE", new { id = vEHICULE.Id }, vEHICULE);
        }

        // DELETE: api/Vehicule/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VEHICULE>> DeleteVEHICULE(Guid id)
        {
            var vEHICULE = await _context.Vehicule.FindAsync(id);
            if (vEHICULE == null)
            {
                return NotFound();
            }

            _context.Vehicule.Remove(vEHICULE);
            await _context.SaveChangesAsync();

            return vEHICULE;
        }

        private bool VEHICULEExists(Guid id)
        {
            return _context.Vehicule.Any(e => e.Id == id);
        }
    }
}
