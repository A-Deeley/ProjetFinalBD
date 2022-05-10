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

INSERT INTO tblCategories
(nom)
VALUES
('Examen'),
('Exercise'),
('Devoir');

INSERT INTO tblStatut
(etat)
VALUES
('En Cours'),
('Terminé');

INSERT into tblSessions
(noProgramme, annee, saison)
VALUES
('420-B0', 2022, 'Hiver');