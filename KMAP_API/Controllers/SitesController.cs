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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin,super-admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly KmapContext _context;

        public SitesController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Sites/idEntreprise
        [Route("GetSitesByEntreprise/{id}/{typePage}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteViewModel>>> GetSitesByEntreprise(Guid id, string typePage)
        {
            var sites = new List<SiteViewModel>();

            var siteRequest = await _context.Site.Include(s => s.Entreprise).Where(s => s.Entreprise.Id == id).Include(u => u.Personnels).Include(u => u.Vehicules).ThenInclude(v => v.Reservations).ToListAsync();

            foreach (var site in siteRequest)
            {
                switch (typePage)
                {
                    case "utilisateur":
                        sites.Add(
                            new SiteViewModel(site)
                            {
                                NbUtilisateurs = site.Personnels.Count
                            }
                        );
                        break;
                    case "reservation":
                        var nbResa = 0;
                        foreach (var v in site.Vehicules)
                        {
                            nbResa += v.Reservations.Count;
                        }

                        sites.Add(new SiteViewModel(site)
                        {
                            NbReservations = nbResa
                        }
                    );
                        break;
                    case "vehicule":
                        sites.Add(
                            new SiteViewModel(site)
                            {
                                NbVehicules = site.Vehicules.Count
                            }
                        );
                        break;
                }

            }
            return sites;
        }

        // GET: api/Sites/5
        [HttpGet("id")]
        public async Task<ActionResult<SiteViewModel>> GetSite(Guid id)
        {
            return new SiteViewModel(await _context.Site.Where(s => s.Id == id).Include(s => s.Entreprise).FirstOrDefaultAsync());
        }

        // PUT: api/Sites/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(Guid id, SiteViewModel siteVM)
        {
            if (!_context.Site.Any(s => s.Id == id))
            {
                return BadRequest();
            }

            var site = _context.Site.FirstOrDefault(s => s.Id == id);

            site.Update(siteVM);

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(SiteViewModel siteVM)
        {
            var e = _context.Entreprise.FirstOrDefault(e => e.Id == siteVM.Entreprise.Id);

            _context.Site.Add(new Site()
            {
                Libelle = siteVM.Libelle,
                Entreprise = e
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Site>> DeleteSite(Guid id)
        {
            var site = await _context.Site.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Site.Remove(site);
            await _context.SaveChangesAsync();

            return Ok();
        }

        #region private function

        private bool SiteExists(Guid id)
        {
            return _context.Site.Any(e => e.Id == id);
        }

        #endregion
    }
}
