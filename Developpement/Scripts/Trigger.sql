use h22_ebd_projet1
DELIMITER //
CREATE Trigger AddCat AFTER Insert On tbltaches
for each row
BEGIN
    INSERT INTO `h22_ebd_projet1`.`tblcategorietaches`(`idCategorie`, `idTache`)
    VALUES(new.idCategorie ,new.idTache);
END //
DELIMITER ;

DELIMITER //
CREATE Trigger upCat after Update On tbltaches
for each row
BEGIN
UPDATE `h22_ebd_projet1`.`tblcategorietaches`
SET `idCategorie` = new.idCategorie
WHERE `idTache` = old.idtache;

END //
DELIMITER ;

DELIMITER //
CREATE Trigger delCatTache before Delete On tbltaches
for each row
BEGIN
DELETE FROM `h22_ebd_projet1`.`tblcategorietaches`
WHERE old.idTache=idTache;

DELETE FROM `h22_ebd_projet1`.`tblrappels`
WHERE old.idTache=idTache;
END //

DELIMITER ;