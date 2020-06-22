using KMAP_API.Data;
using KMAP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "super-admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly KmapContext _context;

        public PersonnelController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Personnel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetPersonnel()
        {
            return await _context.Personnel.ToListAsync();
        }

        // GET: api/Personnel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personnel>> GetPersonnel(Guid id)
        {
            var personnel = await _context.Personnel.FindAsync(id);

            if (personnel == null)
            {
                return NotFound();
            }

            return personnel;
        }

        // PUT: api/Personnel/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonnel(Guid id, Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return BadRequest();
            }

            _context.Entry(personnel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelExists(id))
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

        // POST: api/Personnel
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Personnel>> PostPersonnel(Personnel personnel)
        {
            _context.Personnel.Add(personnel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonnel", new { id = personnel.Id }, personnel);
        }

        // DELETE: api/Personnel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personnel>> DeletePersonnel(Guid id)
        {
            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }

            _context.Personnel.Remove(personnel);
            await _context.SaveChangesAsync();

            return personnel;
        }

        private bool PersonnelExists(Guid id)
        {
            return _context.Personnel.Any(e => e.Id == id);
        }
    }
}
