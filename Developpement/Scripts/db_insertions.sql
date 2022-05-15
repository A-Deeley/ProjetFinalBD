INSERT into tblSessions
VALUES
('Automne_2020'),
('Hiver_2021'),
('Automne_2021'),
('Hiver_2022');

INSERT INTO tblProgrammes
(noProgramme, nomProgramme)
VALUES
('420-B0', "Techniques de l'informatique"),
('410-B0', "Techniques de comptabilité et de gestion"),
('384-A0', "Techniques de recherche et de gestion de données");

INSERT INTO tblCours
(noCours, nomCours, couleur)
VALUES
('410-433-CH', "CULTURES ORGANISATIONNELLES EN INFORMATIQUE", '6887e1'),
('420-D50-CH', "EXPLOITATION DE BASES DE DONNÉES", '47ad4c'),
('420-D51-CH', "PROGRAMMATION WEB TRANSACTIONNELLE", '2156ed'),
('420-D52-CH', "INTRODUCTION AUX SERVICES DE DONNÉES", '75b2b2');

INSERT INTO tblProgrammeSessionCours
(idCours, noProgramme, idSession)
VALUES
(1, '420-B0', 'Hiver_2022'),
(2, '420-B0', 'Hiver_2022'),
(3, '420-B0', 'Hiver_2022'),
(4, '420-B0', 'Hiver_2022');

INSERT INTO tblCategories
(nom)
VALUES
('Examen'),
('Exercise'),
('Devoir');

INSERT INTO tblStatuts
VALUES
(0, 'En Cours'),
(1, 'Terminé');

INSERT INTO tblTaches (idCours, idStatut, titre, dateFin, description) VALUES
('1', 0, 'Test Tache0', '2022-05-13', 'Test Description blah!'),
('1', 0, 'Test Tache1', '2022-05-14', 'Test Description loll!'),
('1', 0, 'Test Tache2', '2022-05-15', 'Test Description ptdr!');

INSERT INTO tblRappels (idTache, dateRappel, titre, message) VALUES
(3, '2022-05-12', 'Test Rappel 3', 'Ceci est un rappel pour la tache 3.');

INSERT INTO tblCategorieTaches
(idTache, idCategorie)
VALUES
(1, 2),
(2, 2),
(3, 3);