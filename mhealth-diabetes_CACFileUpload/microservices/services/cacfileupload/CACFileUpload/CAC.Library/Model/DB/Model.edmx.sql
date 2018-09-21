










































-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/01/2017 19:11:35

-- Generated from EDMX file: D:\GitHub\mhealth-diabetes_CACFileUpload\microservices\services\cacfileupload\CACFileUpload\CAC.Library\Model\DB\Model.edmx
-- Target version: 3.0.0.0

-- --------------------------------------------------


DROP DATABASE IF EXISTS `cactesterdb`;
CREATE DATABASE `cactesterdb`;
USE `cactesterdb`;


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


--    ALTER TABLE `tbl_archivo_cac` DROP CONSTRAINT `FK_tbl_archivo_cac_tbl_usuario`;

--    ALTER TABLE `tbl_paciente_prioritario` DROP CONSTRAINT `FK_tbl_paciente_prioritario_tbl_archivo_cac`;

--    ALTER TABLE `tbl_validacion_archivo` DROP CONSTRAINT `FK_tbl_validacion_archivo_tbl_archivo_cac`;

--    ALTER TABLE `tbl_usuario` DROP CONSTRAINT `FK_tbl_usuario_tbl_organizacion`;

--    ALTER TABLE `tbl_variable_prioritaria` DROP CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_prioritaria`;

--    ALTER TABLE `tbl_usuario_rol` DROP CONSTRAINT `FK_tbl_usuario_rol_tbl_rol`;

--    ALTER TABLE `tbl_usuario_rol` DROP CONSTRAINT `FK_tbl_usuario_rol_tbl_usuario`;

--    ALTER TABLE `tbl_cac` DROP CONSTRAINT `FK_tbl_CAC_tbl_archivo_cac`;

--    ALTER TABLE `tbl_variable_desactualizada` DROP CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_desactualizada`;


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

    DROP TABLE IF EXISTS `tbl_archivo_cac`;

    DROP TABLE IF EXISTS `tbl_organizacion`;

    DROP TABLE IF EXISTS `tbl_paciente_prioritario`;

    DROP TABLE IF EXISTS `tbl_rol`;

    DROP TABLE IF EXISTS `tbl_usuario`;

    DROP TABLE IF EXISTS `tbl_usuario_rol`;

    DROP TABLE IF EXISTS `tbl_validacion_archivo`;

    DROP TABLE IF EXISTS `tbl_variable_prioritaria`;

    DROP TABLE IF EXISTS `tbl_cac`;

    DROP TABLE IF EXISTS `tbl_variable_desactualizada`;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `tbl_archivo_cac`(
	`id` CHAR(36) BINARY NOT NULL, 
	`nombre` varchar (50) NOT NULL, 
	`tamano` varchar (20) NOT NULL, 
	`fechaCreacion` datetime NOT NULL, 
	`numFilasImportadas` int NOT NULL, 
	`urlArchivo` varchar (500) NOT NULL, 
	`idUsuario` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_archivo_cac` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_organizacion`(
	`id` CHAR(36) BINARY NOT NULL, 
	`NIT` varchar (100) NOT NULL, 
	`nombre` varchar (100) NOT NULL, 
	`direccion` varchar (100) NOT NULL, 
	`numTelefonico` varchar (10), 
	`eps` bool NOT NULL);

ALTER TABLE `tbl_organizacion` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_paciente_prioritario`(
	`id` CHAR(36) BINARY NOT NULL, 
	`nombres` varchar (100) NOT NULL, 
	`apellidos` varchar (100) NOT NULL, 
	`cedula` varchar (20) NOT NULL, 
	`numContacto` varchar (10), 
	`idArchivo` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_paciente_prioritario` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_rol`(
	`id` CHAR(36) BINARY NOT NULL, 
	`nombre` varchar (100) NOT NULL, 
	`descripcion` varchar (100));

ALTER TABLE `tbl_rol` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_usuario`(
	`id` CHAR(36) BINARY NOT NULL, 
	`nombres` varchar (100) NOT NULL, 
	`apellidos` varchar (100) NOT NULL, 
	`email` varchar (100) NOT NULL, 
	`numCelular` varchar (10), 
	`idOrganizacion` CHAR(36) BINARY NOT NULL, 
	`uid_firebase` longtext NOT NULL);

ALTER TABLE `tbl_usuario` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_usuario_rol`(
	`id` CHAR(36) BINARY NOT NULL, 
	`idRol` CHAR(36) BINARY NOT NULL, 
	`idUsuario` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_usuario_rol` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_validacion_archivo`(
	`id` CHAR(36) BINARY NOT NULL, 
	`descripcion` varchar (500) NOT NULL, 
	`fechaCreacion` datetime NOT NULL, 
	`idArchivo` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_validacion_archivo` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_variable_prioritaria`(
	`Id` CHAR(36) BINARY NOT NULL, 
	`nombreVariable` longtext NOT NULL, 
	`valorVariable` longtext NOT NULL, 
	`valorUmbral` longtext NOT NULL, 
	`tbl_paciente_prioritario_id` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_variable_prioritaria` ADD PRIMARY KEY (`Id`);





CREATE TABLE `tbl_cac`(
	`id` CHAR(36) BINARY NOT NULL, 
	`idArchivo` CHAR(36) BINARY NOT NULL, 
	`primerNombre` varchar (200) NOT NULL, 
	`segundoNombre` varchar (200) NOT NULL, 
	`primerApellido` varchar (200) NOT NULL, 
	`segundoApellido` varchar (200) NOT NULL, 
	`tipoIdentificacion` varchar (10) NOT NULL, 
	`numIdentificacion` varchar (10) NOT NULL, 
	`fechaNacimiento` datetime NOT NULL, 
	`sexo` varchar (2) NOT NULL, 
	`diagHiperArterial` varchar (5) NOT NULL, 
	`fecDiagHiperArterial` datetime NOT NULL, 
	`diagDiabetesMellitus` varchar (5) NOT NULL, 
	`fecDiadDiabetesMellitus` datetime NOT NULL, 
	`etiologiaCAC` varchar (5) NOT NULL, 
	`PesoKG` varchar (5) NOT NULL, 
	`tallaCtms` varchar (5) NOT NULL, 
	`tensionArtSistolica` varchar (5) NOT NULL, 
	`tensionArtDiastolica` varchar (5) NOT NULL, 
	`creatinina` varchar (5) NOT NULL, 
	`fecUltimaCreatinina` datetime NOT NULL, 
	`hemoglobinaGlicosilada` varchar (5) NOT NULL, 
	`fecHemoGlicosilada` datetime NOT NULL, 
	`albuminuria` varchar (5) NOT NULL, 
	`fecUltimaAlbuminuria` datetime NOT NULL, 
	`creatinuria` varchar (5) NOT NULL, 
	`fecUltimaCreatinuria` datetime NOT NULL, 
	`LDL` varchar (5) NOT NULL, 
	`fecUltimaLDL` datetime NOT NULL, 
	`PTH` varchar (5) NOT NULL, 
	`fechaPTH` datetime NOT NULL, 
	`tasaFiltracionGlomerular` varchar (5) NOT NULL, 
	`diagEnferRenalCronicoERC` varchar (5) NOT NULL, 
	`estadioERC` varchar (5) NOT NULL, 
	`idOrganizacion` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_cac` ADD PRIMARY KEY (`id`);





CREATE TABLE `tbl_variable_desactualizada`(
	`Id` CHAR(36) BINARY NOT NULL, 
	`nombreVariable` longtext NOT NULL, 
	`valorVariable` longtext NOT NULL, 
	`mesesDesactualizado` longtext NOT NULL, 
	`tbl_paciente_prioritario_id` CHAR(36) BINARY NOT NULL);

ALTER TABLE `tbl_variable_desactualizada` ADD PRIMARY KEY (`Id`);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- Creating foreign key on `idUsuario` in table 'tbl_archivo_cac'

ALTER TABLE `tbl_archivo_cac`
ADD CONSTRAINT `FK_tbl_archivo_cac_tbl_usuario`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `tbl_usuario`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_archivo_cac_tbl_usuario'

CREATE INDEX `IX_FK_tbl_archivo_cac_tbl_usuario`
    ON `tbl_archivo_cac`
    (`idUsuario`);



-- Creating foreign key on `idArchivo` in table 'tbl_paciente_prioritario'

ALTER TABLE `tbl_paciente_prioritario`
ADD CONSTRAINT `FK_tbl_paciente_prioritario_tbl_archivo_cac`
    FOREIGN KEY (`idArchivo`)
    REFERENCES `tbl_archivo_cac`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_paciente_prioritario_tbl_archivo_cac'

CREATE INDEX `IX_FK_tbl_paciente_prioritario_tbl_archivo_cac`
    ON `tbl_paciente_prioritario`
    (`idArchivo`);



-- Creating foreign key on `idArchivo` in table 'tbl_validacion_archivo'

ALTER TABLE `tbl_validacion_archivo`
ADD CONSTRAINT `FK_tbl_validacion_archivo_tbl_archivo_cac`
    FOREIGN KEY (`idArchivo`)
    REFERENCES `tbl_archivo_cac`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_validacion_archivo_tbl_archivo_cac'

CREATE INDEX `IX_FK_tbl_validacion_archivo_tbl_archivo_cac`
    ON `tbl_validacion_archivo`
    (`idArchivo`);



-- Creating foreign key on `idOrganizacion` in table 'tbl_usuario'

ALTER TABLE `tbl_usuario`
ADD CONSTRAINT `FK_tbl_usuario_tbl_organizacion`
    FOREIGN KEY (`idOrganizacion`)
    REFERENCES `tbl_organizacion`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_usuario_tbl_organizacion'

CREATE INDEX `IX_FK_tbl_usuario_tbl_organizacion`
    ON `tbl_usuario`
    (`idOrganizacion`);



-- Creating foreign key on `tbl_paciente_prioritario_id` in table 'tbl_variable_prioritaria'

ALTER TABLE `tbl_variable_prioritaria`
ADD CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_prioritaria`
    FOREIGN KEY (`tbl_paciente_prioritario_id`)
    REFERENCES `tbl_paciente_prioritario`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_paciente_prioritariotbl_variable_prioritaria'

CREATE INDEX `IX_FK_tbl_paciente_prioritariotbl_variable_prioritaria`
    ON `tbl_variable_prioritaria`
    (`tbl_paciente_prioritario_id`);



-- Creating foreign key on `idRol` in table 'tbl_usuario_rol'

ALTER TABLE `tbl_usuario_rol`
ADD CONSTRAINT `FK_tbl_usuario_rol_tbl_rol`
    FOREIGN KEY (`idRol`)
    REFERENCES `tbl_rol`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_usuario_rol_tbl_rol'

CREATE INDEX `IX_FK_tbl_usuario_rol_tbl_rol`
    ON `tbl_usuario_rol`
    (`idRol`);



-- Creating foreign key on `idUsuario` in table 'tbl_usuario_rol'

ALTER TABLE `tbl_usuario_rol`
ADD CONSTRAINT `FK_tbl_usuario_rol_tbl_usuario`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `tbl_usuario`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_usuario_rol_tbl_usuario'

CREATE INDEX `IX_FK_tbl_usuario_rol_tbl_usuario`
    ON `tbl_usuario_rol`
    (`idUsuario`);



-- Creating foreign key on `idArchivo` in table 'tbl_cac'

ALTER TABLE `tbl_cac`
ADD CONSTRAINT `FK_tbl_CAC_tbl_archivo_cac`
    FOREIGN KEY (`idArchivo`)
    REFERENCES `tbl_archivo_cac`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CAC_tbl_archivo_cac'

CREATE INDEX `IX_FK_tbl_CAC_tbl_archivo_cac`
    ON `tbl_cac`
    (`idArchivo`);



-- Creating foreign key on `tbl_paciente_prioritario_id` in table 'tbl_variable_desactualizada'

ALTER TABLE `tbl_variable_desactualizada`
ADD CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_desactualizada`
    FOREIGN KEY (`tbl_paciente_prioritario_id`)
    REFERENCES `tbl_paciente_prioritario`
        (`id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_paciente_prioritariotbl_variable_desactualizada'

CREATE INDEX `IX_FK_tbl_paciente_prioritariotbl_variable_desactualizada`
    ON `tbl_variable_desactualizada`
    (`tbl_paciente_prioritario_id`);



-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
