use h22_ebd_projet1;
## Nombres taches pour aujourdhui
DELIMITER //
drop procedure if exists TacheAuj //
CREATE PROCEDURE  TacheAuj
()
BEGIN
Select count(idTache) from tblTaches where datefin = current_date();
END //
DELIMITER ;

call TacheAuj();

##-----------------------------------------------
## Nombres taches pour aujourdhui d'après la catégorie
DELIMITER //
drop procedure if exists TacheAujCat //
CREATE PROCEDURE  TacheAujCat
(in idCat integer)
BEGIN
Select count(cat.idTache) from TacheAujourdhui as tache
JOIN tblcategorietaches as cat ON tache.idTache = cat.idTache
where idCat = cat.idCategorie ;
END //
DELIMITER ;

##call TacheAujCat(2);

##-----------------------------------------------
## Nombres taches pour aujourdhui d'après le cours
DELIMITER //
drop procedure if exists TacheAujCours //
CREATE PROCEDURE  TacheAujCours
(in idCours integer)
BEGIN
Select count(tache.idTache) from TacheAujourdhui as tache
where  tache.idCours = idCours;
END //
DELIMITER ;

##call TacheAujCours(1);

##-----------------------------------------------
## les rappels pour aujourdhui 
DELIMITER //
drop procedure if exists RappelTache //
CREATE PROCEDURE  RappelTache
()
BEGIN
Select rappels.idRappel, rappels.dateRappel, rappels.titre, rappels.message, cours.couleur from tblrappels as rappels
join TacheAujourdhui  as taches on rappels.idTache =  taches.idTache
join tblCours as cours on taches.idCours = cours.idCours;
END //
DELIMITER ;

##call RappelTache();

##-----------------------------------------------
## les rappels pour aujourdhui 
DELIMITER //
drop procedure if exists RappelCours //
CREATE PROCEDURE  RappelCours
(in idCours integer)
BEGIN
Select rappels.idRappel, rappels.dateRappel, rappels.titre, rappels.message, cours.couleur from tblrappels as rappels
join TacheAujourdhui  as taches on rappels.idTache =  taches.idTache
join tblCours as cours on taches.idCours = cours.idCours
where cours.idCours = idCours;
END //
DELIMITER ;

call RappelCours(1);
