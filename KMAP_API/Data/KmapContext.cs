using KMAP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KMAP_API.Data
{
    public class KmapContext : DbContext
    {

        //Command
        //Aller dans la console de Gestionnaire de pacakage
        //Add-Migration nomdelamigration
        //Update-Database

        public KmapContext(DbContextOptions<KmapContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personnel_Reservation>()
                .HasKey(pr => new { pr.PersonnelId, pr.ReservationID });
        }

        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Cle> Cle { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Site> Site { get; set; }
        public DbSet<Vehicule> Vehicule { get; set; }
        public DbSet<Personnel_Reservation> Personnel_Reservations { get; set; }
        public DbSet<Notification> Notification { get; set; }

    }
}
