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
(noCours, nomCours)
VALUES
('410-433-CH', "CULTURES ORGANISATIONNELLES EN INFORMATIQUE"),
('420-D50-CH', "EXPLOITATION DE BASES DE DONNÉES"),
('420-D51-CH', "PROGRAMMATION WEB TRANSACTIONNELLE"),
('420-D52-CH', "INTRODUCTION AUX SERVICES DE DONNÉES");

INSERT INTO tblProgrammeCours
(noProgramme, noCours)
VALUES
('420-B0', '420-433-CH'),
('420-B0', '420-D50-CH'),
('420-B0', '420-D51-CH'),
('420-B0', '420-D52-CH');

INSERT INTO tblCategories
(nom)
VALUES
('Examen'),
('Exercise'),
('Devoir');

INSERT INTO tblStatuts
(etat)
VALUES
('En Cours'),
('Terminé');

INSERT INTO tblSessionProgrammes
VALUES
('Automne_2020', '420-B0'),
('Hiver_2021', '420-B0'),
('Automne_2021', '420-B0'),
('Hiver_2022', '420-B0');