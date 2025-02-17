﻿// <auto-generated />
using System;
using KMAP_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KMAP_API.Migrations
{
    [DbContext(typeof(KmapContext))]
    [Migration("20200618133224_newModel")]
    partial class newModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("KMAP_API.Models.Cle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Libelle")
                        .HasColumnType("text");

                    b.Property<Guid?>("VehiculeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehiculeId");

                    b.ToTable("Cle");
                });

            modelBuilder.Entity("KMAP_API.Models.Entreprise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Libelle")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Entreprise");
                });

            modelBuilder.Entity("KMAP_API.Models.Personnel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .HasColumnType("text");

                    b.Property<string>("Permis")
                        .HasColumnType("text");

                    b.Property<string>("Prenom")
                        .HasColumnType("text");

                    b.Property<Guid?>("SiteId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Personnel");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Personnel");
                });

            modelBuilder.Entity("KMAP_API.Models.Personnel_Reservation", b =>
                {
                    b.Property<Guid>("PersonnelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReservationID")
                        .HasColumnType("uuid");

                    b.HasKey("PersonnelId", "ReservationID");

                    b.HasIndex("ReservationID");

                    b.ToTable("Personnel_Reservations");
                });

            modelBuilder.Entity("KMAP_API.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CleId")
                        .HasColumnType("uuid");

                    b.Property<bool>("ConfirmationCle")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("SiteDestination")
                        .HasColumnType("text");

                    b.Property<Guid?>("UtilisateurId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("VehiculeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CleId");

                    b.HasIndex("UtilisateurId");

                    b.HasIndex("VehiculeId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("KMAP_API.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Libelle")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("KMAP_API.Models.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EntrepriseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Libelle")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EntrepriseId");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("KMAP_API.Models.Vehicule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Actif")
                        .HasColumnType("boolean");

                    b.Property<string>("Modele")
                        .HasColumnType("text");

                    b.Property<int>("NbPlaces")
                        .HasColumnType("integer");

                    b.Property<int>("NbPortes")
                        .HasColumnType("integer");

                    b.Property<string>("NumImmat")
                        .HasColumnType("text");

                    b.Property<Guid?>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeCarbu")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Vehicule");
                });

            modelBuilder.Entity("KMAP_API.Models.Utilisateur", b =>
                {
                    b.HasBaseType("KMAP_API.Models.Personnel");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.HasIndex("RoleId");

                    b.HasDiscriminator().HasValue("Utilisateur");
                });

            modelBuilder.Entity("KMAP_API.Models.Cle", b =>
                {
                    b.HasOne("KMAP_API.Models.Vehicule", "Vehicule")
                        .WithMany("Cles")
                        .HasForeignKey("VehiculeId");
                });

            modelBuilder.Entity("KMAP_API.Models.Personnel", b =>
                {
                    b.HasOne("KMAP_API.Models.Site", "Site")
                        .WithMany("Personnels")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("KMAP_API.Models.Personnel_Reservation", b =>
                {
                    b.HasOne("KMAP_API.Models.Personnel", "Personnel")
                        .WithMany("Personnel_Reservations")
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KMAP_API.Models.Reservation", "Reservation")
                        .WithMany("Personnel_Reservations")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KMAP_API.Models.Reservation", b =>
                {
                    b.HasOne("KMAP_API.Models.Cle", "Cle")
                        .WithMany()
                        .HasForeignKey("CleId");

                    b.HasOne("KMAP_API.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId");

                    b.HasOne("KMAP_API.Models.Vehicule", "Vehicule")
                        .WithMany("Reservations")
                        .HasForeignKey("VehiculeId");
                });

            modelBuilder.Entity("KMAP_API.Models.Site", b =>
                {
                    b.HasOne("KMAP_API.Models.Entreprise", "Entreprise")
                        .WithMany("Sites")
                        .HasForeignKey("EntrepriseId");
                });

            modelBuilder.Entity("KMAP_API.Models.Vehicule", b =>
                {
                    b.HasOne("KMAP_API.Models.Site", "Site")
                        .WithMany("Vehicules")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("KMAP_API.Models.Utilisateur", b =>
                {
                    b.HasOne("KMAP_API.Models.Role", "Role")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
