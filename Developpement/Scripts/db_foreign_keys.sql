ALTER TABLE tblProgrammeSessionCours
ADD FOREIGN KEY (idSession) REFERENCES tblSessions(idSession),
ADD FOREIGN KEY (noProgramme) REFERENCES tblProgrammes(noProgramme),
ADD FOREIGN KEY (noCours) REFERENCES tblCours(noCours);

ALTER TABLE tblTaches
ADD FOREIGN KEY (noCours) REFERENCES tblCours(noCours),
ADD FOREIGN KEY (idStatut) REFERENCES tblStatuts(idStatut);

ALTER TABLE tblRappels
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);

ALTER TABLE tblCategorieTaches
ADD FOREIGN KEY (nomCategorie) REFERENCES tblCategories(nom),
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);