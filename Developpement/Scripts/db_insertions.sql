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
(noProgramme, noCours, idSession)
VALUES
('420-B0', '410-433-CH', 'Hiver_2022'),
('420-B0', '420-D50-CH', 'Hiver_2022'),
('420-B0', '420-D51-CH', 'Hiver_2022'),
('420-B0', '420-D52-CH', 'Hiver_2022');

INSERT INTO tblCategories
VALUES
('Examen'),
('Exercise'),
('Devoir');

INSERT INTO tblStatuts
VALUES
(0, 'En Cours'),
(1, 'Terminé');