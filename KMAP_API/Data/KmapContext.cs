using KMAP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KMAP_API.Data
{
    public class KmapContext : DbContext
    {
        public KmapContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationUtilisateur>()
                .HasKey(ru => new { ru.IdUtilisateur, ru.IdReservation });
            modelBuilder.Entity<ReservationUtilisateur>()
                .HasOne(ru => ru.Reservation)
                .WithMany(r => r.ReservationUtilisateurs)
                .HasForeignKey(ru => ru.IdReservation);
            modelBuilder.Entity<ReservationUtilisateur>()
                .HasOne(ru => ru.Utilisateur)
                .WithMany(u => u.ReservationUtilisateurs)
                .HasForeignKey(ru => ru.IdUtilisateur);
        }

        public DbSet<UTILISATEUR> Utilisateur { get; set; }
        public DbSet<CLE> Cle { get; set; }
        public DbSet<ENTREPRISE> Entreprise { get; set; }
        public DbSet<PERSONNEL> Personnel { get; set; }
        public DbSet<RESERVATION> Reservation { get; set; }
        public DbSet<ROLE> Role { get; set; }
        public DbSet<ReservationUtilisateur> ReservationUtilisateur { get; set; }
        public DbSet<SITE> Site { get; set; }
        public DbSet<VEHICULE> Vehicule { get; set; }

    }
}
