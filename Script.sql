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
	VALUES ('49a46fa6-007f-42cd-9319-23eb0c012c14', '111-AAA-111', 'Clio', 5, 5, 'Essence', true, '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423');
INSERT INTO public."Vehicule"("Id", "NumImmat", "Modele", "NbPlaces", "NbPortes", "TypeCarbu", "Actif", "SiteId")
	VALUES ('1fa6da4d-8d86-4499-86f5-efb0bf7114ab', '222-BBB-444', 'Clio', 5, 5, 'Essence', true, '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423');
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
	VALUES ('27a51826-0f74-42f0-b3a7-3f51246545e6', 'BON', 'Jean', 'jean.bon@eni.fr', 'pochetteSurprise1', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'utilisateur', '123456', 'a4828836-eff3-4151-b1b9-ab5d6a3cd3ca');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('dfdb5d6a-540b-4aea-a61e-ff18d44cb8ff', 'LAFOND', 'Michel', 'michel.lafond@eni.fr', 'pochetteSurprise2', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'utilisateur', '123456', '5280a0cb-71ed-4757-b1fd-f3f595dee92b');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator")
	VALUES ('62b473a0-91f0-4e6f-bea7-1953ac199157', 'ALAPLAGE', 'Martine', 'martine.alaplage@eni.fr', 'trotinette', '6b6d75b0-0a7f-4ef4-89ce-e6f671d6f423', 'personnel');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('f39ebf9d-cb2e-40ff-8885-48bddbb829c9', 'MINET', 'Pierre', 'pierre.minet@eni.fr', 'pochetteSurprise3', '0d441381-e25a-4486-a910-ab3bedeb47ea', 'utilisateur', '123456', '5280a0cb-71ed-4757-b1fd-f3f595dee92b');
INSERT INTO public."Personnel"( "Id", "Nom", "Prenom", "Mail", "Permis", "SiteId", "Discriminator", "Password", "RoleId")
	VALUES ('5151123e-725b-447c-a11a-ac1728c0f595', 'admin', 'kmap', 'kmap.admin@kmap.fr', 'fusée', '9c60d56a-aef1-412f-a8c4-11d480432153', 'utilisateur', 'kmap_pass', '4a3b872b-ae9f-4b04-89ca-ddbb26e2dc25');

-- Reservation

INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId")
	VALUES ('ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a', 'Rennes', true, '2020/01/01 09:00:00', '2020/01/08 18:00:00', 'Réunion rennes', 'dfdb5d6a-540b-4aea-a61e-ff18d44cb8ff', '49a46fa6-007f-42cd-9319-23eb0c012c14', '975938d6-8599-4384-b5c3-9d6ec159f755');
INSERT INTO public."Reservation"("Id", "SiteDestination", "ConfirmationCle", "DateDebut", "DateFin", "Description", "UtilisateurId", "VehiculeId", "CleId")
	VALUES ('e4c0c87b-84e9-4726-a19f-7b3f4a27eb1d', 'Angers', false, '2020/04/01 09:00:00', '2020/04/08 18:00:00', 'Visite Angers', 'dfdb5d6a-540b-4aea-a61e-ff18d44cb8ff', '1fa6da4d-8d86-4499-86f5-efb0bf7114ab', '93cef5b6-9231-44bb-bf88-e54a7107c7cf');

-- Personnel_Reservation

INSERT INTO public."Personnel_Reservations"("PersonnelId", "ReservationID") VALUES ('62b473a0-91f0-4e6f-bea7-1953ac199157', 'ac4e6ceb-57cd-4a60-81f4-53717b0fdf0a');
INSERT INTO public."Personnel_Reservations"("PersonnelId", "ReservationID") VALUES ('27a51826-0f74-42f0-b3a7-3f51246545e6', 'e4c0c87b-84e9-4726-a19f-7b3f4a27eb1d');
