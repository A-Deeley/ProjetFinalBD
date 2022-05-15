ALTER TABLE tblProgrammeSessionCours
ADD FOREIGN KEY (idSession) REFERENCES tblSessions(idSession),
ADD FOREIGN KEY (noProgramme) REFERENCES tblProgrammes(noProgramme),
ADD FOREIGN KEY (idCours) REFERENCES tblCours(idCours);

ALTER TABLE tblTaches
ADD FOREIGN KEY (idCours) REFERENCES tblCours(idCours),
ADD FOREIGN KEY (idStatut) REFERENCES tblStatuts(idStatut);

ALTER TABLE tblRappels
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);

ALTER TABLE tblCategorieTaches
ADD FOREIGN KEY (idCategorie) REFERENCES tblCategories(id),
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);