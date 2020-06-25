using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KMAP_API.Data;
using KMAP_API.Models;
using KMAP_API.ViewModels;

namespace KMAP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClesController : ControllerBase
    {
        private readonly KmapContext _context;

        public ClesController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Cles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleViewModel>>> GetCle()
        {
            var c = new List<CleViewModel>();
            foreach (var cle in await _context.Cle.Include(c => c.Vehicule).ToListAsync())
            {
                c.Add(new CleViewModel(cle));
            }
            return c;
        }

        // GET: api/Cles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleViewModel>> GetCle(Guid id)
        {
            var cle = new CleViewModel(await _context.Cle.Include(c => c.Vehicule).FirstOrDefaultAsync(c => c.Id == id));

            if (cle == null)
            {
                return NotFound();
            }

            return cle;
        }

        // PUT: api/Cles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCle(Guid id, Cle cle)
        {
            if (id != cle.Id)
            {
                return BadRequest();
            }

            _context.Entry(cle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleExists(id))
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

        // POST: api/Cles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cle>> PostCle(Cle cle)
        {
            _context.Cle.Add(cle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCle", new { id = cle.Id }, cle);
        }

        // DELETE: api/Cles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cle>> DeleteCle(Guid id)
        {
            var cle = await _context.Cle.FindAsync(id);
            if (cle == null)
            {
                return NotFound();
            }

            _context.Cle.Remove(cle);
            await _context.SaveChangesAsync();

            return cle;
        }

        private bool CleExists(Guid id)
        {
            return _context.Cle.Any(e => e.Id == id);
        }
    }
}
