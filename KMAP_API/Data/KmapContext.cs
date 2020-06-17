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

        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Cle> Cle { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ReservationUtilisateur> ReservationUtilisateur { get; set; }
        public DbSet<Site> Site { get; set; }
        public DbSet<VEHICULE> Vehicule { get; set; }

    }
}
