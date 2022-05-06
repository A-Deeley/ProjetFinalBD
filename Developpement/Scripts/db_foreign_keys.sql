ALTER TABLE tblSessions
ADD FOREIGN KEY (noProgramme) REFERENCES tblProgrammes(noProgramme);

ALTER TABLE tblProgrammeCours
ADD FOREIGN KEY (noProgramme) REFERENCES tblProgrammes(noProgramme),
ADD FOREIGN KEY (noCours) REFERENCES tblCours(noCours);

ALTER TABLE tblTaches
ADD FOREIGN KEY (noCours) REFERENCES tblCours(noCours),
ADD FOREIGN KEY (idStatut) REFERENCES tblStatus(idStatut);

ALTER TABLE tblRappels
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);

ALTER TABLE tblLabelTaches
ADD FOREIGN KEY (idLabel) REFERENCES tblLabel(idLabel),
ADD FOREIGN KEY (idTache) REFERENCES tblTaches(idTache);