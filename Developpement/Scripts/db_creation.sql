USE h22_ebd_projet1;
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS tblSessions;
CREATE TABLE tblSessions(
idSession VARCHAR(12) PRIMARY KEY
);

DROP TABLE IF EXISTS tblProgrammeSessionCours;
CREATE TABLE tblProgrammeSessionCours(
idSession VARCHAR(12) NOT NULL,
noProgramme VARCHAR(6) NOT NULL,
idCours INT NOT NULL,
PRIMARY KEY (idSession, noProgramme, idCours)
);

DROP TABLE IF EXISTS tblProgrammes;
CREATE TABLE tblProgrammes(
noProgramme VARCHAR(6) PRIMARY KEY,
nomProgramme VARCHAR(50) NOT NULL
);

DROP TABLE IF EXISTS tblCours;
CREATE TABLE tblCours(
idCours INT PRIMARY KEY AUTO_INCREMENT,
noCours VARCHAR(15),
nomCours VARCHAR(50) NOT NULL,
description VARCHAR(255),
couleur CHAR(6)
);

DROP TABLE IF EXISTS tblTaches;
CREATE TABLE tblTaches(
idTache INT PRIMARY KEY AUTO_INCREMENT,
idCours INT NOT NULL,
idStatut INT NOT NULL,
titre VARCHAR(32) NOT NULL,
dateDebut DATETIME,
dateFin DATETIME NOT NULL,
description VARCHAR(255)
);

DROP TABLE IF EXISTS tblStatuts;
CREATE TABLE tblStatuts(
idStatut INT PRIMARY KEY,
etat VARCHAR(16) NOT NULL
);

DROP TABLE IF EXISTS tblRappels;
CREATE TABLE tblRappels(
idRappel INT PRIMARY KEY AUTO_INCREMENT,
idTache INT NOT NULL,
dateRappel DATETIME NOT NULL,
titre VARCHAR(32) NOT NULL,
message VARCHAR(255)
);

DROP TABLE IF EXISTS tblCategorieTaches;
CREATE TABLE tblCategorieTaches(
idCategorie INT NOT NULL,
idTache INT NOT NULL,
PRIMARY KEY (idCategorie, idTache)
);

DROP TABLE IF EXISTS tblCategories;
CREATE TABLE tblCategories(
id INT AUTO_INCREMENT PRIMARY KEY,
nom VARCHAR(16)
);
SET FOREIGN_KEY_CHECKS = 1;