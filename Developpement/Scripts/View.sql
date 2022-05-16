use h22_ebd_projet1;
## Afficher les taches du jour.
drop view if exists TacheAujourdhui;
CREATE VIEW TacheAujourdhui
AS 
SELECT Tache.idTache ,Tache.Titre, Tache.DateDebut, Tache.DateFin, Tache.description, Statut.etat, Tache.idCategorie ,Cours.couleur, Cours.Nocours  FROM tblTaches as Tache
join tblcours as Cours on Tache.idCours = Cours.idCours
join tblStatuts as Statut on Tache.idStatut = Statut.idStatut 
where Date(datefin) = current_date();

Select * from  TacheAujourdhui;

##---------------------------------------------
## Afficher les Rappels du jour.
drop view if exists RappelAujourdhui;
CREATE VIEW RappelAujourdhui
AS 
SELECT *
FROM
tblRappels
where Date(dateRappel) = current_date();

Select * from  RappelAujourdhui;