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
using KMAP_API.ViewModels;

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

        // GET: api/Notifications/GetNotificationsByReservation
        [Route("GetNotificationsByReservation/{idReservation}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationViewModel>>> GetNotificationsByReservation(Guid idReservation)
        {
            var n = new List<NotificationViewModel>();
            foreach (var notification in await _context.Notification.Include(n => n.Utilisateur).Include(n => n.Reservation).Where(n => n.Reservation.Id == idReservation).ToListAsync())
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
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
