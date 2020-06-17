using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KMAP_API.Data;
using KMAP_API.Models;

namespace KMAP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly KmapContext _context;

        public UtilisateurController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UTILISATEUR>>> GetUtilisateur()
        {
            return await _context.Utilisateur.ToListAsync();
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UTILISATEUR>> GetUTILISATEUR(Guid id)
        {
            var uTILISATEUR = await _context.Utilisateur.FindAsync(id);

            if (uTILISATEUR == null)
            {
                return NotFound();
            }

            return uTILISATEUR;
        }

        // PUT: api/Utilisateur/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUTILISATEUR(Guid id, UTILISATEUR uTILISATEUR)
        {
            if (id != uTILISATEUR.Id)
            {
                return BadRequest();
            }

            _context.Entry(uTILISATEUR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UTILISATEURExists(id))
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

        // POST: api/Utilisateur
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UTILISATEUR>> PostUTILISATEUR(UTILISATEUR uTILISATEUR)
        {
            _context.Utilisateur.Add(uTILISATEUR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUTILISATEUR", new { id = uTILISATEUR.Id }, uTILISATEUR);
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UTILISATEUR>> DeleteUTILISATEUR(Guid id)
        {
            var uTILISATEUR = await _context.Utilisateur.FindAsync(id);
            if (uTILISATEUR == null)
            {
                return NotFound();
            }

            _context.Utilisateur.Remove(uTILISATEUR);
            await _context.SaveChangesAsync();

            return uTILISATEUR;
        }

        private bool UTILISATEURExists(Guid id)
        {
            return _context.Utilisateur.Any(e => e.Id == id);
        }
    }
}
