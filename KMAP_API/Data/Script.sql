-- Drop all
DROP TABLE IF EXISTS public."Entreprise" CASCADE;
DROP TABLE IF EXISTS public."Site" CASCADE;
DROP TABLE IF EXISTS public."Vehicule" CASCADE;
DROP TABLE IF EXISTS public."Cle" CASCADE;
DROP TABLE IF EXISTS public."Role" CASCADE;
DROP TABLE IF EXISTS public."Personnel" CASCADE;
DROP TABLE IF EXISTS public."Reservation" CASCADE;
DROP TABLE IF EXISTS public."Personnel_Reservations" CASCADE;
DROP TABLE IF EXISTS public."Notification" CASCADE;
DROP TABLE IF EXISTS public."__EFMigrationsHistory" CASCADE;

----------------------------------------------------------------------------------------------------------
-- Create tables

-- Table: public."Entreprise"
CREATE TABLE public."Entreprise"
(
    "Id" uuid NOT NULL,
    "Libelle" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Entreprise" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Entreprise"
    OWNER to kmap_admin;

-- Table: public."Site"
CREATE TABLE public."Site"
(
    "Id" uuid NOT NULL,
    "Libelle" text COLLATE pg_catalog."default",
    "EntrepriseId" uuid,
    CONSTRAINT "PK_Site" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Site_Entreprise_EntrepriseId" FOREIGN KEY ("EntrepriseId")
        REFERENCES public."Entreprise" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)
TABLESPACE pg_default;

ALTER TABLE public."Site"
    OWNER to kmap_admin;

-- Table: public."Vehicule"
CREATE TABLE public."Vehicule"
(
    "Id" uuid NOT NULL,
    "NumImmat" text COLLATE pg_catalog."default",
    "Modele" text COLLATE pg_catalog."default",
    "NbPlaces" integer NOT NULL,
    "NbPortes" integer NOT NULL,
    "TypeCarbu" text COLLATE pg_catalog."default",
    "Actif" boolean NOT NULL,
    "SiteId" uuid,
    CONSTRAINT "PK_Vehicule" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Vehicule_Site_SiteId" FOREIGN KEY ("SiteId")
        REFERENCES public."Site" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Vehicule"
    OWNER to kmap_admin;

-- Table: public."Cle"
CREATE TABLE public."Cle"
(
    "Id" uuid NOT NULL,
    "Libelle" text COLLATE pg_catalog."default",
    "VehiculeId" uuid,
    CONSTRAINT "PK_Cle" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Cle_Vehicule_VehiculeId" FOREIGN KEY ("VehiculeId")
        REFERENCES public."Vehicule" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Cle"
    OWNER to kmap_admin;

-- Table: public."Role"
CREATE TABLE public."Role"
(
    "Id" uuid NOT NULL,
    "Libelle" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Role" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Role"
    OWNER to kmap_admin;

-- Table: public."Personnel"
CREATE TABLE public."Personnel"
(
    "Id" uuid NOT NULL,
    "Nom" text COLLATE pg_catalog."default",
    "Prenom" text COLLATE pg_catalog."default",
    "Mail" text COLLATE pg_catalog."default",
    "Permis" text COLLATE pg_catalog."default",
    "SiteId" uuid,
    "Discriminator" text COLLATE pg_catalog."default" NOT NULL,
    "Password" text COLLATE pg_catalog."default",
    "RoleId" uuid,
    CONSTRAINT "PK_Personnel" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Personnel_Role_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES public."Role" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Personnel_Site_SiteId" FOREIGN KEY ("SiteId")
        REFERENCES public."Site" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Personnel"
    OWNER to kmap_admin;

-- Table: public."Reservation"
CREATE TABLE public."Reservation"
(
    "Id" uuid NOT NULL,
    "SiteDestination" text COLLATE pg_catalog."default",
    "ConfirmationCle" boolean NOT NULL,
    "DateDebut" timestamp without time zone NOT NULL,
    "DateFin" timestamp without time zone NOT NULL,
    "Description" text COLLATE pg_catalog."default",
    "UtilisateurId" uuid,
    "VehiculeId" uuid,
    "CleId" uuid,
    "IsAccepted" boolean NOT NULL,
    "IsRejeted" boolean NOT NULL,
    CONSTRAINT "PK_Reservation" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Reservation_Cle_CleId" FOREIGN KEY ("CleId")
        REFERENCES public."Cle" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Reservation_Personnel_UtilisateurId" FOREIGN KEY ("UtilisateurId")
        REFERENCES public."Personnel" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Reservation_Vehicule_VehiculeId" FOREIGN KEY ("VehiculeId")
        REFERENCES public."Vehicule" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Reservation"
    OWNER to kmap_admin;

-- Table: public."Notification"
CREATE TABLE public."Notification"
(
    "Id" uuid NOT NULL,
    "UtilisateurId" uuid,
    "ReservationId" uuid,
    "DateNotif" timestamp without time zone NOT NULL,
    "TypeNotif" integer NOT NULL,
    "Commentaire" text COLLATE pg_catalog."default",
    "Checked" boolean NOT NULL,
    CONSTRAINT "PK_Notification" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Notification_Personnel_UtilisateurId" FOREIGN KEY ("UtilisateurId")
        REFERENCES public."Personnel" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Notification_Reservation_ReservationId" FOREIGN KEY ("ReservationId")
        REFERENCES public."Reservation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Notification"
    OWNER to kmap_admin;

-- Table: public."Personnel_Reservations"
CREATE TABLE public."Personnel_Reservations"
(
    "PersonnelId" uuid NOT NULL,
    "ReservationID" uuid NOT NULL,
    CONSTRAINT "PK_Personnel_Reservations" PRIMARY KEY ("PersonnelId", "ReservationID"),
    CONSTRAINT "FK_Personnel_Reservations_Personnel_PersonnelId" FOREIGN KEY ("PersonnelId")
        REFERENCES public."Personnel" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_Personnel_Reservations_Reservation_ReservationID" FOREIGN KEY ("ReservationID")
        REFERENCES public."Reservation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE public."Personnel_Reservations"
    OWNER to kmap_admin;




----------------------------------------------------------------------------------------------------------
-- Index 

-- Index: IX_Site_EntrepriseId
CREATE INDEX "IX_Site_EntrepriseId"
    ON public."Site" USING btree
    ("EntrepriseId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Vehicule_SiteId
CREATE INDEX "IX_Vehicule_SiteId"
    ON public."Vehicule" USING btree
    ("SiteId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Cle_VehiculeId
CREATE INDEX "IX_Cle_VehiculeId"
    ON public."Cle" USING btree
    ("VehiculeId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Personnel_RoleId
CREATE INDEX "IX_Personnel_RoleId"
    ON public."Personnel" USING btree
    ("RoleId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Personnel_SiteId
CREATE INDEX "IX_Personnel_SiteId"
    ON public."Personnel" USING btree
    ("SiteId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Reservation_CleId
CREATE INDEX "IX_Reservation_CleId"
    ON public."Reservation" USING btree
    ("CleId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Reservation_UtilisateurId
CREATE INDEX "IX_Reservation_UtilisateurId"
    ON public."Reservation" USING btree
    ("UtilisateurId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Reservation_VehiculeId
CREATE INDEX "IX_Reservation_VehiculeId"
    ON public."Reservation" USING btree
    ("VehiculeId" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Index: IX_Personnel_Reservations_ReservationID
CREATE INDEX "IX_Personnel_Reservations_ReservationID"
    ON public."Personnel_Reservations" USING btree
    ("ReservationID" ASC NULLS LAST)
    TABLESPACE pg_default;

----------------------------------------------------------------------------------------------------------
-- Add data

-- Entreprise
INSERT INTO public."Entreprise"("Id", "Libelle") VALUES('253ff149-f310-403e-a4ab-0b6722d941db', 'ENI');
INSERT INTO public."Entreprise"("Id", "Libelle") VALUES('10912df1-f5d2-44e9-98ea-8dd417c744be', 'KMAP');

-- Site
INSERT INTO public."Site"("Id", "Libelle", "EntrepriseId") 
    VALUES ('6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'Nantes', '253ff149-f310-403e-a4ab-0b6722d941db');
INSERT INTO public."Site"("Id", "Libelle", "EntrepriseId") 
    VALUES ('0d441381-e25a-4486-a910-ab3bedeb47ea', 'Rennes', '253ff149-f310-403e-a4ab-0b6722d941db');
INSERT INTO public."Site"("Id", "Libelle", "EntrepriseId") 
    VALUES ('9c60d56a-aef1-412f-a8c4-11d480432153', 'Siege', '10912df1-f5d2-44e9-98ea-8dd417c744be');

-- Vehicule
INSERT INTO public."Vehicule"("Id", "NumImmat", "Modele", "NbPlaces", "NbPortes", "TypeCarbu", "Actif", "SiteId")
	VALUES ('49a46fa6-007f-42cd-9319-23eb0c012c14', '111-AAA-111', '206', 3, 2, 'Essence', true, '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423');
INSERT INTO public."Vehicule"("Id", "NumImmat", "Modele", "NbPlaces", "NbPortes", "TypeCarbu", "Actif", "SiteId")
	VALUES ('1fa6da4d-8d86-4499-86f5-efb0bf7114ab', '222-BBB-444', '207', 5, 5, 'Essence', true, '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423');
INSERT INTO public."Vehicule"("Id", "NumImmat", "Modele", "NbPlaces", "NbPortes", "TypeCarbu", "Actif", "SiteId")
	VALUES ('dc7c75a3-a476-4dd7-a1eb-d1d46b0801c6', '333-CCC-444', 'Clio', 5, 5, 'Essence', false, '0d441381-e25a-4486-a910-ab3bedeb47ea');
INSERT INTO public."Vehicule"("Id", "NumImmat", "Modele", "NbPlaces", "NbPortes", "TypeCarbu", "Actif", "SiteId")
	VALUES ('7b6884d2-3afd-40de-bc48-98e979f41c6f', '444-DDD-444', 'Audi A7', 5, 5, 'Essence', true, '9c60d56a-aef1-412f-a8c4-11d480432153');

-- Cle
INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('975938d6-8599-4384-b5c3-9d6ec159f755', 'Clé bleu', '49a46fa6-007f-42cd-9319-23eb0c012c14');
INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('5e2e29b3-de11-4c5e-82e1-5d898c0db3b0', 'Clé rouge', '49a46fa6-007f-42cd-9319-23eb0c012c14');

INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('93cef5b6-9231-44bb-bf88-e54a7107c7cf', 'Clé bleu', '1fa6da4d-8d86-4499-86f5-efb0bf7114ab');
INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('f691680d-4e63-447a-9426-e191efa2ee64', 'Clé rouge', '1fa6da4d-8d86-4499-86f5-efb0bf7114ab');

INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('22a1891c-b99e-4f89-bf73-d0a7f8fa1296', 'Clé bleu', 'dc7c75a3-a476-4dd7-a1eb-d1d46b0801c6');
INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('779ebee3-9c7a-49ed-bd9a-b45f8d3038fe', 'Clé rouge', 'dc7c75a3-a476-4dd7-a1eb-d1d46b0801c6');

INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('22406a03-1edf-4b36-97c1-5bbee14b0d5c', 'Clé 1', '7b6884d2-3afd-40de-bc48-98e979f41c6f');
INSERT INTO public."Cle"("Id", "Libelle", "VehiculeId") 
    VALUES ('9db52ef3-1e0f-4828-91ef-47a4bba3b01a', 'Clé 2', '7b6884d2-3afd-40de-bc48-98e979f41c6f');

-- Role
INSERT INTO public."Role"("Id", "Libelle") VALUES ('5280a0cb-71ed-4757-b1fd-f3f595dee92b', 'user');
INSERT INTO public."Role"("Id", "Libelle") VALUES ('a4828836-eff3-4151-b1b9-ab5d6a3cd3ca', 'admin');
INSERT INTO public."Role"("Id", "Libelle") VALUES ('4a3b872b-ae9f-4b04-89ca-ddbb26e2dc25', 'super-admin');

-- Personnel
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('27a51826-0f74-42f0-b3a7-3f51246545e6', 'BON', 'Jean', 'jean.bon@eni.fr', 'pochetteSurprise1', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'Utilisateur', '$2y$12$S4g.mojd0kh.bRk2uCxBL.GX3zR2jZ1tQWBLokzVPAzjcEAa.ipve', 'a4828836-eff3-4151-b1b9-ab5d6a3cd3ca');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('dfdb5d6a-540b-4aea-a61e-ff18d44cb8ff', 'LAFOND', 'Michel', 'michel.lafond@eni.fr', 'pochetteSurprise2', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'Utilisateur', '$2y$12$S4g.mojd0kh.bRk2uCxBL.GX3zR2jZ1tQWBLokzVPAzjcEAa.ipve', '5280a0cb-71ed-4757-b1fd-f3f595dee92b');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator")
	VALUES ('62b473a0-91f0-4e6f-bea7-1953ac199157', 'ALAPLAGE', 'Martine', 'martine.alaplage@eni.fr', 'trotinette', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'Personnel');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('f39ebf9d-cb2e-40ff-8885-48bddbb829c9', 'MINET', 'Pierre', 'pierre.minet@eni.fr', 'pochetteSurprise3', '0d441381-e25a-4486-a910-ab3bedeb47ea', 'Utilisateur', '$2y$12$S4g.mojd0kh.bRk2uCxBL.GX3zR2jZ1tQWBLokzVPAzjcEAa.ipve', '5280a0cb-71ed-4757-b1fd-f3f595dee92b');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('5151123e-725b-447c-a11a-ac1728c0f595', 'admin', 'kmap', 'kmap.admin@kmap.fr', 'fusée', '9c60d56a-aef1-412f-a8c4-11d480432153', 'Utilisateur', '$2y$12$YsfgI2N5Z.eA8uVezGpXWObWcsBfwjFiWne/qWm1NE6UGVcnJYh2i', '4a3b872b-ae9f-4b04-89ca-ddbb26e2dc25');

-- Reservation

INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId", "IsAccepted", "IsRejeted")
	VALUES ('ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a', 'Rennes', true, '2020/09/01 09:00:00', '2020/09/05 15:00:00', 'Réunion rennes', '27a51826-0f74-42f0-b3a7-3f51246545e6', '49a46fa6-007f-42cd-9319-23eb0c012c14', '975938d6-8599-4384-b5c3-9d6ec159f755', false, false);
INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId", "IsAccepted", "IsRejeted")
	VALUES ('e4c0c87b-84e9-4726-a19f-7b3f4a27eb1d', 'Angers', false, '2020/09/04 15:00:00', '2020/09/08 15:00:00', 'Visite Angers', '27a51826-0f74-42f0-b3a7-3f51246545e6', '1fa6da4d-8d86-4499-86f5-efb0bf7114ab', '93cef5b6-9231-44bb-bf88-e54a7107c7cf', true, false);
INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId", "IsAccepted", "IsRejeted")
	VALUES ('d4ae6c6e-eb6f-11ea-adc1-0242ac120002', 'Angers', false, '2020/09/08 09:00:00', '2020/09/10 09:00:00', 'Visite Angers', '27a51826-0f74-42f0-b3a7-3f51246545e6', '49a46fa6-007f-42cd-9319-23eb0c012c14', '93cef5b6-9231-44bb-bf88-e54a7107c7cf', false, false);
INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId", "IsAccepted", "IsRejeted")
	VALUES ('e456d584-eb6f-11ea-adc1-0242ac120002', 'Angers', false, '2020/09/08 15:00:00', '2020/09/15 09:00:00', 'Visite Angers', '27a51826-0f74-42f0-b3a7-3f51246545e6', '1fa6da4d-8d86-4499-86f5-efb0bf7114ab', '93cef5b6-9231-44bb-bf88-e54a7107c7cf', false, true);

-- Notification

INSERT INTO public."Notification"("Id", "UtilisateurId", "ReservationId", "DateNotif", "TypeNotif", "Commentaire", "Checked")
	VALUES ('f31c1ea2-3c4e-43f8-af16-5d0770f83d8e', '27a51826-0f74-42f0-b3a7-3f51246545e6', 'ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a', '2020/08/20 09:00:00', 1, 'Changer la voiture', true);
INSERT INTO public."Notification"("Id", "UtilisateurId", "ReservationId", "DateNotif", "TypeNotif", "Checked")
	VALUES ('93cf905f-f25e-4e4d-839e-ee9dcbf88f11', '27a51826-0f74-42f0-b3a7-3f51246545e6', 'ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a', '2020/08/22 09:00:00', 2, false);
INSERT INTO public."Notification"("Id", "UtilisateurId", "ReservationId", "DateNotif", "TypeNotif", "Commentaire", "Checked")
	VALUES ('f052761e-c3d6-47dd-86d3-6e73a5662d75', '27a51826-0f74-42f0-b3a7-3f51246545e6', 'ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a', '2020/08/25 09:00:00', 3, 'RU annulé', false);

-- Personnel_Reservation

INSERT INTO public."Personnel_Reservations"("PersonnelId", "ReservationID") VALUES ('62b473a0-91f0-4e6f-bea7-1953ac199157', 'ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a');
INSERT INTO public."Personnel_Reservations"("PersonnelId", "ReservationID") VALUES ('dfdb5d6a-540b-4aea-a61e-ff18d44cb8ff', 'e4c0c87b-84e9-4726-a19f-7b3f4a27eb1d');
