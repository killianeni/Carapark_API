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
    public class UtilisateursController : ControllerBase
    {
        private readonly KmapContext _context;

        public UtilisateursController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs/GetUtilisateursbyEntreprise
        [Route("GetUtilisateursbyEntreprise/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurViewModel>>> GetUtilisateursbyEntreprise(Guid id)
        {
            var u = new List<UtilisateurViewModel>();
            foreach (var utilisateur in await _context.Utilisateur.Include(u => u.Role).Include(u => u.Site).ThenInclude(u => u.Entreprise).Where(u => u.Site.Entreprise.Id == id).ToListAsync())
            {
                u.Add(new UtilisateurViewModel(utilisateur));
            }
            return u;
        }

        // GET: api/Utilisateurs/GetUtilisateursbySite
        [Route("GetUtilisateursbySite/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurViewModel>>> GetUtilisateursbySite(Guid id)
        {
            var u = new List<UtilisateurViewModel>();
            foreach (var utilisateur in await _context.Utilisateur.Include(u => u.Role).Include(u => u.Site).ThenInclude(u => u.Entreprise).Where(u => u.Site.Id == id).ToListAsync())
            {
                u.Add(new UtilisateurViewModel(utilisateur));
            }
            return u;
        }

        // GET: api/Utilisateurs/5
        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurViewModel>> GetUtilisateur(Guid id)
        {
            var utilisateur = new UtilisateurViewModel(await _context.Utilisateur.Include(u => u.Role).Include(u => u.Site).ThenInclude(u => u.Entreprise).FirstOrDefaultAsync(u => u.Id == id));

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(Guid id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // POST: api/Utilisateurs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateur.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Utilisateur>> DeleteUtilisateur(Guid id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return utilisateur;
        }

        private bool UtilisateurExists(Guid id)
        {
            return _context.Utilisateur.Any(e => e.Id == id);
        }
    }
}
