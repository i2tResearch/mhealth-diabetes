-- --------------------------------------------------------
-- Host:                         40.71.81.33
-- Server version:               5.5.54-0ubuntu0.14.04.1 - (Ubuntu)
-- Server OS:                    debian-linux-gnu
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for mhealthdiabetesdb
CREATE DATABASE IF NOT EXISTS `mhealthdiabetesdb` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `mhealthdiabetesdb`;

-- Dumping structure for table mhealthdiabetesdb.tbl_archivo_cac
CREATE TABLE IF NOT EXISTS `tbl_archivo_cac` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `tamano` varchar(20) NOT NULL,
  `fechaCreacion` datetime NOT NULL,
  `numFilasImportadas` int(11) NOT NULL,
  `urlArchivo` varchar(500) NOT NULL,
  `idUsuario` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_archivo_cac_tbl_usuario` (`idUsuario`),
  CONSTRAINT `FK_tbl_archivo_cac_tbl_usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tbl_usuario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_cac
CREATE TABLE IF NOT EXISTS `tbl_cac` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `idArchivo` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `primerNombre` varchar(200) NOT NULL,
  `segundoNombre` varchar(200) NOT NULL,
  `primerApellido` varchar(200) NOT NULL,
  `segundoApellido` varchar(200) NOT NULL,
  `tipoIdentificacion` varchar(10) NOT NULL,
  `numIdentificacion` varchar(10) NOT NULL,
  `fechaNacimiento` date NOT NULL,
  `sexo` varchar(2) NOT NULL,
  `diagHiperArterial` varchar(5) NOT NULL,
  `fecDiagHiperArterial` date NOT NULL,
  `diagDiabetesMellitus` varchar(5) NOT NULL,
  `fecDiadDiabetesMellitus` date NOT NULL,
  `etiologiaCAC` varchar(5) NOT NULL,
  `PesoKG` varchar(5) NOT NULL,
  `tallaCtms` varchar(5) NOT NULL,
  `tensionArtSistolica` varchar(5) NOT NULL,
  `tensionArtDiastolica` varchar(5) NOT NULL,
  `creatinina` varchar(5) NOT NULL,
  `fecUltimaCreatinina` date NOT NULL,
  `hemoglobinaGlicosilada` varchar(5) NOT NULL,
  `fecHemoGlicosilada` date NOT NULL,
  `albuminuria` varchar(5) NOT NULL,
  `fecUltimaAlbuminuria` date NOT NULL,
  `creatinuria` varchar(5) NOT NULL,
  `fecUltimaCreatinuria` date NOT NULL,
  `LDL` varchar(5) NOT NULL,
  `fecUltimaLDL` date NOT NULL,
  `PTH` varchar(5) NOT NULL,
  `fechaPTH` date NOT NULL,
  `tasaFiltracionGlomerular` varchar(5) NOT NULL,
  `diagEnferRenalCronicoERC` varchar(5) NOT NULL,
  `estadioERC` varchar(5) NOT NULL,
  `idOrganizacion` char(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_CAC_tbl_archivo_cac` (`idArchivo`),
  KEY `estadioERC` (`estadioERC`),
  KEY `idOrganizacion` (`idOrganizacion`),
  CONSTRAINT `FK_tbl_CAC_tbl_archivo_cac` FOREIGN KEY (`idArchivo`) REFERENCES `tbl_archivo_cac` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_organizacion
CREATE TABLE IF NOT EXISTS `tbl_organizacion` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `NIT` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `numTelefonico` varchar(10) DEFAULT NULL,
  `eps` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_paciente_prioritario
CREATE TABLE IF NOT EXISTS `tbl_paciente_prioritario` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombres` varchar(100) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `cedula` varchar(20) NOT NULL,
  `numContacto` varchar(10) DEFAULT NULL,
  `idArchivo` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_paciente_prioritario_tbl_archivo_cac` (`idArchivo`),
  CONSTRAINT `FK_tbl_paciente_prioritario_tbl_archivo_cac` FOREIGN KEY (`idArchivo`) REFERENCES `tbl_archivo_cac` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_rol
CREATE TABLE IF NOT EXISTS `tbl_rol` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_usuario
CREATE TABLE IF NOT EXISTS `tbl_usuario` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombres` varchar(100) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `numCelular` varchar(10) DEFAULT NULL,
  `idOrganizacion` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `uid_firebase` longtext NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_usuario_tbl_organizacion` (`idOrganizacion`),
  CONSTRAINT `FK_tbl_usuario_tbl_organizacion` FOREIGN KEY (`idOrganizacion`) REFERENCES `tbl_organizacion` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_usuario_rol
CREATE TABLE IF NOT EXISTS `tbl_usuario_rol` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `idRol` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `idUsuario` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_usuario_rol_tbl_rol` (`idRol`),
  KEY `IX_FK_tbl_usuario_rol_tbl_usuario` (`idUsuario`),
  CONSTRAINT `FK_tbl_usuario_rol_tbl_rol` FOREIGN KEY (`idRol`) REFERENCES `tbl_rol` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tbl_usuario_rol_tbl_usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tbl_usuario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_validacion_archivo
CREATE TABLE IF NOT EXISTS `tbl_validacion_archivo` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `descripcion` varchar(500) NOT NULL,
  `fechaCreacion` datetime NOT NULL,
  `idArchivo` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_validacion_archivo_tbl_archivo_cac` (`idArchivo`),
  CONSTRAINT `FK_tbl_validacion_archivo_tbl_archivo_cac` FOREIGN KEY (`idArchivo`) REFERENCES `tbl_archivo_cac` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_variable_desactualizada
CREATE TABLE IF NOT EXISTS `tbl_variable_desactualizada` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombreVariable` longtext NOT NULL,
  `valorVariable` longtext NOT NULL,
  `mesesDesactualizado` longtext NOT NULL,
  `tbl_paciente_prioritario_id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FK_tbl_paciente_prioritariotbl_variable_desactualizada` (`tbl_paciente_prioritario_id`),
  CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_desactualizada` FOREIGN KEY (`tbl_paciente_prioritario_id`) REFERENCES `tbl_paciente_prioritario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table mhealthdiabetesdb.tbl_variable_prioritaria
CREATE TABLE IF NOT EXISTS `tbl_variable_prioritaria` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `nombreVariable` longtext NOT NULL,
  `valorVariable` longtext NOT NULL,
  `valorUmbral` longtext NOT NULL,
  `tbl_paciente_prioritario_id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FK_tbl_paciente_prioritariotbl_variable_prioritaria` (`tbl_paciente_prioritario_id`),
  CONSTRAINT `FK_tbl_paciente_prioritariotbl_variable_prioritaria` FOREIGN KEY (`tbl_paciente_prioritario_id`) REFERENCES `tbl_paciente_prioritario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for procedure mhealthdiabetesdb.Prc_ControlDiabetesMellitus
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ControlDiabetesMellitus`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT







,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.diagDiabetesMellitus = 1
		And tbl_cac.hemoglobinaGlicosilada < 7
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN
		And tbl_cac.fecHemoGlicosilada > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN
		And tbl_cac.fecHemoGlicosilada > fechaFin;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_ControlHipertensionArterial
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ControlHipertensionArterial`(
	IN `intervalo_IN` INT
,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare numerador, denominador Int;	
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.tensionArtSistolica < 140
		And tbl_cac.tensionArtDiastolica < 90;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1);
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_ControlLDL
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ControlLDL`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT




,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.LDL <= 100
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaLDL < fechaIni_IN
		And tbl_cac.fecUltimaLDL > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaLDL < fechaIni_IN;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_ControlPTH
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ControlPTH`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT

,
	IN `estadioERC_IN` INT,
	IN `PTHMayor_IN` INT,
	IN `PTHMenor_IN` INT

,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.PTH >= PTHMayor_IN
		And tbl_cac.PTH <= PTHMenor_IN
		And tbl_cac.fechaPTH < fechaIni_IN
		And tbl_cac.fechaPTH > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fechaPTH < fechaIni_IN
		And tbl_cac.fechaPTH > fechaFin;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_MedicionAlbuminuria
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_MedicionAlbuminuria`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT




,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaAlbuminuria < fechaIni_IN
		And tbl_cac.fecUltimaAlbuminuria > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaAlbuminuria < fechaIni_IN;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_MedicionCreatinina
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_MedicionCreatinina`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT



,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.fecUltimaCreatinina <> '1800-01-01'
		And tbl_cac.fecUltimaCreatinina <> ' 1845-01-01'
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN
		And tbl_cac.fecUltimaCreatinina > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN;		
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_MedicionHbA1c
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_MedicionHbA1c`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT





,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)		
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN
		And tbl_cac.fecHemoGlicosilada > fechaFin;
		
	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.diagDiabetesMellitus = 1		
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_MedicionLDL
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_MedicionLDL`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT




,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.fecUltimaLDL < fechaIni_IN
		And tbl_cac.fecUltimaLDL > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.fecUltimaLDL < fechaIni_IN;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_MedicionPTH
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_MedicionPTH`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT


,
	IN `estadioERC_IN` INT

,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fechaPTH < fechaIni_IN
		And tbl_cac.fechaPTH > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1);
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

-- Dumping structure for procedure mhealthdiabetesdb.Prc_ProgresionEnfermedadRenal
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ProgresionEnfermedadRenal`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT





,
	IN `idOrganizacion_IN` CHAR(36)
)
Begin

	Declare fechaFin DateTime;
	Declare numerador, denominador Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into numerador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.tasaFiltracionGlomerular < 5
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN
		And tbl_cac.fecUltimaCreatinina > fechaFin;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN;
		
	Select (numerador / denominador) as resultado;
	
End//
DELIMITER ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
