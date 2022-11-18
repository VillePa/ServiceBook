-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema huoltokirja
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema huoltokirja
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `huoltokirja` DEFAULT CHARACTER SET utf8 ;
USE `huoltokirja` ;

-- -----------------------------------------------------
-- Table `huoltokirja`.`kayttaja`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`kayttaja` (
  `idkayttaja` INT NOT NULL AUTO_INCREMENT,
  `nimi` VARCHAR(45) NOT NULL,
  `kayttajatunnus` VARCHAR(45) NOT NULL,
  `salasana` VARCHAR(200) NOT NULL,
  `luotu` DATETIME NOT NULL,
  `viimeisin_kirjautuminen` DATETIME NULL,
  `rooli` VARCHAR(10) NOT NULL,
  `salasanaSalt` VARCHAR(200) NULL,
  `poistettu` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`idkayttaja`),
  UNIQUE INDEX `idkayttaja_UNIQUE` (`idkayttaja` ASC) VISIBLE,
  UNIQUE INDEX `kayttajatunnus_UNIQUE` (`kayttajatunnus` ASC) VISIBLE,
  UNIQUE INDEX `salasana_UNIQUE` (`salasana` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`kohderyhma`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`kohderyhma` (
  `idkohderyhma` INT NOT NULL AUTO_INCREMENT,
  `nimi` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idkohderyhma`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`kohde`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`kohde` (
  `idkohde` INT NOT NULL AUTO_INCREMENT,
  `nimi` VARCHAR(45) NOT NULL,
  `kuvaus` VARCHAR(45) NOT NULL,
  `sijainti` VARCHAR(45) NOT NULL,
  `tyyppi` VARCHAR(45) NOT NULL,
  `malli` VARCHAR(45) NOT NULL,
  `tunnus` VARCHAR(45) NOT NULL,
  `tila` VARCHAR(45) NOT NULL,
  `luotu` DATETIME NOT NULL,
  `idkayttaja` INT NOT NULL,
  `idkohderyhma` INT NOT NULL,
  PRIMARY KEY (`idkohde`, `idkayttaja`, `idkohderyhma`),
  UNIQUE INDEX `idkohde_UNIQUE` (`idkohde` ASC) VISIBLE,
  INDEX `fk_kohde_kayttaja_idx` (`idkayttaja` ASC) VISIBLE,
  INDEX `fk_kohde_kohderyhma1_idx` (`idkohderyhma` ASC) VISIBLE,
  CONSTRAINT `fk_kohde_kayttaja`
    FOREIGN KEY (`idkayttaja`)
    REFERENCES `huoltokirja`.`kayttaja` (`idkayttaja`),
  CONSTRAINT `fk_kohde_kohderyhma1`
    FOREIGN KEY (`idkohderyhma`)
    REFERENCES `huoltokirja`.`kohderyhma` (`idkohderyhma`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`auditointi`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`auditointi` (
  `idauditointi` INT NOT NULL AUTO_INCREMENT,
  `luotu` DATETIME NOT NULL,
  `selite` VARCHAR(45) NULL DEFAULT NULL,
  `lopputulos` INT NULL DEFAULT NULL,
  `idkohde` INT NOT NULL,
  `idkayttaja` INT NOT NULL,
  PRIMARY KEY (`idauditointi`, `idkohde`, `idkayttaja`),
  INDEX `fk_auditointi_kohde1_idx` (`idkohde` ASC) VISIBLE,
  INDEX `fk_auditointi_kayttaja1_idx` (`idkayttaja` ASC) VISIBLE,
  CONSTRAINT `fk_auditointi_kohde1`
    FOREIGN KEY (`idkohde`)
    REFERENCES `huoltokirja`.`kohde` (`idkohde`),
  CONSTRAINT `fk_auditointi_kayttaja1`
    FOREIGN KEY (`idkayttaja`)
    REFERENCES `huoltokirja`.`kayttaja` (`idkayttaja`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`auditointipohja`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`auditointipohja` (
  `idauditointipohja` INT NOT NULL AUTO_INCREMENT,
  `selite` VARCHAR(45) NULL DEFAULT NULL,
  `luontiaika` DATETIME NULL DEFAULT NULL,
  `idkayttaja` INT NOT NULL,
  `idkohderyhma` INT NOT NULL,
  PRIMARY KEY (`idauditointipohja`, `idkayttaja`, `idkohderyhma`),
  INDEX `fk_auditointipohja_kayttaja1_idx` (`idkayttaja` ASC) VISIBLE,
  INDEX `fk_auditointipohja_kohderyhma1_idx` (`idkohderyhma` ASC) VISIBLE,
  CONSTRAINT `fk_auditointipohja_kayttaja1`
    FOREIGN KEY (`idkayttaja`)
    REFERENCES `huoltokirja`.`kayttaja` (`idkayttaja`),
  CONSTRAINT `fk_auditointipohja_kohderyhma1`
    FOREIGN KEY (`idkohderyhma`)
    REFERENCES `huoltokirja`.`kohderyhma` (`idkohderyhma`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`tarkastus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`tarkastus` (
  `idtarkastus` INT NOT NULL AUTO_INCREMENT,
  `aikaleima` DATETIME NOT NULL,
  `syy` VARCHAR(45) NOT NULL,
  `havainnot` VARCHAR(100) NOT NULL,
  `tilan_muutos` INT NOT NULL,
  `idkayttaja` INT NOT NULL,
  `idkohde` INT NOT NULL,
  PRIMARY KEY (`idtarkastus`, `idkayttaja`, `idkohde`),
  INDEX `fk_tarkastus_kayttaja1_idx` (`idkayttaja` ASC) VISIBLE,
  INDEX `fk_tarkastus_kohde1_idx` (`idkohde` ASC) VISIBLE,
  CONSTRAINT `fk_tarkastus_kayttaja1`
    FOREIGN KEY (`idkayttaja`)
    REFERENCES `huoltokirja`.`kayttaja` (`idkayttaja`),
  CONSTRAINT `fk_tarkastus_kohde1`
    FOREIGN KEY (`idkohde`)
    REFERENCES `huoltokirja`.`kohde` (`idkohde`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`vaatimuspohja`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`vaatimuspohja` (
  `idvaatimuspohja` INT NOT NULL AUTO_INCREMENT,
  `kuvaus` VARCHAR(45) NULL DEFAULT NULL,
  `pakollisuus` VARCHAR(45) NULL DEFAULT NULL,
  `idauditointipohja` INT NOT NULL,
  PRIMARY KEY (`idvaatimuspohja`, `idauditointipohja`),
  INDEX `fk_vaatimus_auditointipohja1_idx` (`idauditointipohja` ASC) VISIBLE,
  CONSTRAINT `fk_vaatimus_auditointipohja1`
    FOREIGN KEY (`idauditointipohja`)
    REFERENCES `huoltokirja`.`auditointipohja` (`idauditointipohja`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`vaatimus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`vaatimus` (
  `idvaatimus` INT NOT NULL AUTO_INCREMENT,
  `kuvaus` VARCHAR(45) NULL DEFAULT NULL,
  `pakollisuus` VARCHAR(45) NULL DEFAULT NULL,
  `taytetty` INT NULL DEFAULT NULL,
  `idauditointi` INT NOT NULL,
  PRIMARY KEY (`idvaatimus`, `idauditointi`),
  INDEX `fk_vaatimus_auditointi1_idx` (`idauditointi` ASC) VISIBLE,
  CONSTRAINT `fk_vaatimus_auditointi1`
    FOREIGN KEY (`idauditointi`)
    REFERENCES `huoltokirja`.`auditointi` (`idauditointi`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `huoltokirja`.`liite`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `huoltokirja`.`liite` (
  `idliite` INT NOT NULL AUTO_INCREMENT,
  `sijainti` VARCHAR(100) NULL,
  `idtarkastus` INT NOT NULL,
  PRIMARY KEY (`idliite`, `idtarkastus`),
  INDEX `fk_liite_tarkastus1_idx` (`idtarkastus` ASC) VISIBLE,
  CONSTRAINT `fk_liite_tarkastus1`
    FOREIGN KEY (`idtarkastus`)
    REFERENCES `huoltokirja`.`tarkastus` (`idtarkastus`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
