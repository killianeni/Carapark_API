﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMAP_API.Migrations
{
    public partial class InitialEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Libelle = table.Column<string>(nullable: true),
                    ENTREPRISEId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Entreprise_ENTREPRISEId",
                        column: x => x.ENTREPRISEId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NumImmat = table.Column<string>(nullable: true),
                    Modele = table.Column<string>(nullable: true),
                    NbPlaces = table.Column<int>(nullable: false),
                    NbPortes = table.Column<int>(nullable: false),
                    TypeCarbu = table.Column<string>(nullable: true),
                    Actif = table.Column<bool>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicule_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Libelle = table.Column<string>(nullable: true),
                    VehiculeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cle_Vehicule_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteDestination = table.Column<string>(nullable: true),
                    ConfirmationCle = table.Column<bool>(nullable: false),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    HeureDebut = table.Column<DateTime>(nullable: false),
                    Heurefin = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UtilisateurId = table.Column<Guid>(nullable: true),
                    VehiculeId = table.Column<Guid>(nullable: true),
                    CleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Cle_CleId",
                        column: x => x.CleId,
                        principalTable: "Cle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Vehicule_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Permis = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RESERVATIONId = table.Column<Guid>(nullable: true),
                    SITEId = table.Column<Guid>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnel_Reservation_RESERVATIONId",
                        column: x => x.RESERVATIONId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnel_Site_SITEId",
                        column: x => x.SITEId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnel_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationUtilisateur",
                columns: table => new
                {
                    IdUtilisateur = table.Column<Guid>(nullable: false),
                    IdReservation = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationUtilisateur", x => new { x.IdUtilisateur, x.IdReservation });
                    table.ForeignKey(
                        name: "FK_ReservationUtilisateur_Reservation_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationUtilisateur_Personnel_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cle_VehiculeId",
                table: "Cle",
                column: "VehiculeId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_RESERVATIONId",
                table: "Personnel",
                column: "RESERVATIONId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_SITEId",
                table: "Personnel",
                column: "SITEId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_RoleId",
                table: "Personnel",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CleId",
                table: "Reservation",
                column: "CleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UtilisateurId",
                table: "Reservation",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_VehiculeId",
                table: "Reservation",
                column: "VehiculeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationUtilisateur_IdReservation",
                table: "ReservationUtilisateur",
                column: "IdReservation");

            migrationBuilder.CreateIndex(
                name: "IX_Site_ENTREPRISEId",
                table: "Site",
                column: "ENTREPRISEId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicule_SiteId",
                table: "Vehicule",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Personnel_UtilisateurId",
                table: "Reservation",
                column: "UtilisateurId",
                principalTable: "Personnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cle_Vehicule_VehiculeId",
                table: "Cle");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Vehicule_VehiculeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Reservation_RESERVATIONId",
                table: "Personnel");

            migrationBuilder.DropTable(
                name: "ReservationUtilisateur");

            migrationBuilder.DropTable(
                name: "Vehicule");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Cle");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Entreprise");
        }
    }
}
