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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "user,admin,super-admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly KmapContext _context;

        public NotificationsController(KmapContext context)
        {
            _context = context;
        }

        // GET: api/Notifications/GetNotificationsByUser
        [Route("GetNotificationsByUser/{idUser}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationViewModel>>> GetNotificationsByUser(Guid idUser)
        {
            var n = new List<NotificationViewModel>();
            foreach (var notification in await _context.Notification.Include(n => n.Utilisateur).Where(n => n.Utilisateur.Id == idUser).Include(n => n.Reservation).ToListAsync())
            {
                n.Add(new NotificationViewModel(notification));
            }

            return n;
        }

        // POST: api/Notifications/AddCommentNotif
        [Route("AddCommentNotif")]
        [HttpPost]
        public async Task<IActionResult> AddCommentNotif(NotificationViewModel notificationVM)
        {
            var user = await _context.Utilisateur.FirstOrDefaultAsync(u => u.Id == notificationVM.IdUser);
            var resa = await _context.Reservation.FirstOrDefaultAsync(r => r.Id == notificationVM.IdResa);

            var notif = new Notification()
            {
                Utilisateur = user,
                Reservation = resa,
                DateNotif = DateTime.Now,
                TypeNotif = State.Waiting,
                Commentaire = notificationVM.Commentaire
            };

            _context.Notification.Add(notif);

            return Ok();
        }

        // PUT: api/CheckNotif/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("CheckNotif/{id}")]
        [HttpPut]
        public async Task<IActionResult> CheckNotif(Guid id, NotificationViewModel notificationVM)
        {
            if (!_context.Notification.Any(p => p.Id == id))
            {
                return BadRequest();
            }

            var notification = _context.Notification.Where(r => r.Id == id).FirstOrDefault();
            notification.Update(notificationVM);

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        #region private function

        private bool NotificationExists(Guid id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }

        #endregion
    }
}
