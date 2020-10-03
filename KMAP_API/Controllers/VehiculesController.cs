﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMAP_API.Data;
using KMAP_API.Models;
using KMAP_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KMAP_API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "user, admin,super-admin")]
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
            foreach (var vehicule in await _context.Vehicule
                                            .Where(v => v.Site.Id == id)
                                            .Include(v => v.Cles)
                                            .Include(v => v.Site)
                                            .OrderByDescending(v => v.Actif).ThenByDescending(v => v.NbPlaces)
                                            .ToListAsync())
            {
                v.Add(new VehiculeViewModel(vehicule));
            }
            return v;
        }


        // GET: api/Vehicules/GetVehiculeNonResaBySiteAndDate
        [Authorize(Roles = "user,admin,super-admin")]
        [HttpGet("GetVehiculesNonResaBySiteAndDate/{id}/{dateDebut}/{dateFin}")]
        public async Task<ActionResult<IEnumerable<VehiculeViewModel>>> GetVehiculesNonResaBySiteAndDate(Guid id, DateTime dateDebut, DateTime dateFin)
        {
            var v = new List<VehiculeViewModel>();
            var listVehiculeReserve = ListVehiculeReserve(dateDebut, dateFin);
            foreach (var vehicule in await _context.Vehicule
                                            .Where(v => v.Site.Id == id && !listVehiculeReserve.Contains(v.Id) && v.Actif)
                                            .Include(v => v.Cles)
                                            .Include(v => v.Site)
                                            .OrderByDescending(v => v.NbPlaces)
                                            .ToListAsync())
            {
                v.Add(new VehiculeViewModel(vehicule));
            }
            return v;
        }

        // GET: api/Vehicules/CountVehiculeActifBySite
        [Authorize(Roles = "user,admin,super-admin")]
        [HttpGet("CountVehiculesActifBySite/{id}")]
        public int CountVehiculeActifBySite(Guid id)
        {
            return _context.Vehicule.Where(v => v.Site.Id == id && v.Actif == true).Count();
        }

        // GET: api/Vehicules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculeViewModel>> GetVehicule(Guid id)
        {
            var vehicule = new VehiculeViewModel(await _context.Vehicule
                                                        .Include(v => v.Cles)
                                                        .Include(v => v.Site)
                                                        .FirstOrDefaultAsync(v => v.Id == id));

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
        public async Task<IActionResult> PutVehicule(Guid id, VehiculeViewModel vehiculeVM)
        {
            if (!_context.Vehicule.Any(v => v.Id == id))
            {
                return BadRequest();
            }

            var vehicule = _context.Vehicule.FirstOrDefault(s => s.Id == id);

            var cles = new List<Cle>();
            if (vehiculeVM.Cles.Count > 0)
            {
                foreach (var cleVM in vehiculeVM.Cles)
                {
                    var cle = _context.Cle.FirstOrDefault(c => c.Id == cleVM.Id);
                    cle.Update(cleVM);
                    cles.Add(cle);
                }
                vehicule.Cles = cles;
            }

            vehicule.Update(vehiculeVM);

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

            return Ok();
        }

        // POST: api/Vehicules
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vehicule>> PostVehicule(VehiculeViewModel vehiculeVM)
        {
            var site = _context.Site.FirstOrDefault(s => s.Id == vehiculeVM.IdSite);

            var cles = new List<Cle>();
            if (vehiculeVM.Cles.Count > 0)
            {
                foreach (var cleVM in vehiculeVM.Cles)
                {
                    var cle = new Cle()
                    {
                        Libelle = cleVM.Libelle
                    };
                    cles.Add(cle);
                    _context.Cle.Add(cle);
                }
            }

            _context.Vehicule.Add(new Vehicule()
            {
                NumImmat = vehiculeVM.NumImmat,
                Modele = vehiculeVM.Modele,
                NbPlaces = vehiculeVM.NbPlaces,
                NbPortes = vehiculeVM.NbPortes,
                TypeCarbu = vehiculeVM.TypeCarbu,
                Actif = vehiculeVM.Actif,
                Site = site,
                Cles = cles
            });

            await _context.SaveChangesAsync();

            return Ok();
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

            return Ok();
        }

        #region private function

        private bool VehiculeExists(Guid id)
        {
            return _context.Vehicule.Any(e => e.Id == id);
        }

        private HashSet<Guid> ListVehiculeReserve(DateTime dateDebut, DateTime dateFin)
        {
            return _context.Reservation.Where(r =>
                (r.DateFin >= dateDebut && r.DateFin <= dateFin) ||
                (r.DateDebut >= dateDebut && r.DateDebut <= dateFin) ||
                (r.DateDebut <= dateDebut && r.DateFin >= dateFin) && r.IsRejeted == false).Include(r => r.Vehicule).Select(r => r.Vehicule.Id).ToHashSet();
        }

        #endregion
    }
}
