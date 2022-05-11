USE h22_ebd_projet1;
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS tblSessions;
CREATE TABLE tblSessions(
idSession VARCHAR(12) PRIMARY KEY
);

DROP TABLE IF EXISTS tblSessionProgrammes;
CREATE TABLE tblSessionProgrammes(
idSession VARCHAR(12),
noProgramme VARCHAR(6),
PRIMARY KEY (idSession, noProgramme)
);

DROP TABLE IF EXISTS tblProgrammes;
CREATE TABLE tblProgrammes(
noProgramme VARCHAR(6) PRIMARY KEY,
nomProgramme VARCHAR(50) NOT NULL
);

DROP TABLE IF EXISTS tblProgrammeCours;
CREATE TABLE tblProgrammeCours(
noProgramme VARCHAR(6),
noCours VARCHAR(15),
PRIMARY KEY (noProgramme, noCours)
);

DROP TABLE IF EXISTS tblCours;
CREATE TABLE tblCours(
noCours VARCHAR(15) PRIMARY KEY,
nomCours VARCHAR(50) NOT NULL,
description VARCHAR(255),
couleur CHAR(6)
);

DROP TABLE IF EXISTS tblTaches;
CREATE TABLE tblTaches(
idTache INT PRIMARY KEY AUTO_INCREMENT,
noCours VARCHAR(15),
idStatut INT NOT NULL,
titre VARCHAR(32) NOT NULL,
dateDebut DATETIME,
dateFin DATETIME NOT NULL,
description VARCHAR(255)
);

DROP TABLE IF EXISTS tblStatuts;
CREATE TABLE tblStatuts(
idStatut INT PRIMARY KEY AUTO_INCREMENT,
etat VARCHAR(16) NOT NULL
);

DROP TABLE IF EXISTS tblRappels;
CREATE TABLE tblRappels(
idRappel INT PRIMARY KEY AUTO_INCREMENT,
idTache INT,
dateRappel DATETIME NOT NULL,
titre VARCHAR(32) NOT NULL,
message VARCHAR(255)
);

DROP TABLE IF EXISTS tblCategorieTaches;
CREATE TABLE tblCategorieTaches(
idCategorie INT,
idTache INT
);

DROP TABLE IF EXISTS tblCategories;
CREATE TABLE tblCategories(
idCategorie INT PRIMARY KEY AUTO_INCREMENT,
nom VARCHAR(16) NOT NULL
);
SET FOREIGN_KEY_CHECKS = 1;