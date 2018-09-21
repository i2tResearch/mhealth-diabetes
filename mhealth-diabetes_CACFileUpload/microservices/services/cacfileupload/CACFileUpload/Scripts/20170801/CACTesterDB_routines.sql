-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: 200.3.193.10    Database: CACTesterDB
-- ------------------------------------------------------
-- Server version	5.7.18-0ubuntu0.16.04.1-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping routines for database 'CACTesterDB'
--
/*!50003 DROP PROCEDURE IF EXISTS `Prc_Albuminuria` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_Albuminuria`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)


)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstudiados, pacientesEstadios Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaAlbuminuria < fechaIni_IN
		And tbl_cac.fecUltimaAlbuminuria > fechaFin;

	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaAlbuminuria < fechaIni_IN;
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		-1 pacientesControlados,
		(pacientesEstadios - pacientesEstudiados)  noMedidos,
		-1 vigentesControlados,
		-1 vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ControlDiabetesMellitus` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ControlDiabetesMellitus`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ControlHipertensionArterial` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ControlHipertensionArterial`(
	IN `fechaIni_IN` DATE,
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
		And tbl_cac.tensionArtDiastolica < 90
		And tbl_cac.fechaRegistro < fechaIni_IN;

	Select
		Count(*) NumPacientes Into denominador
	From
	 	tbl_cac
	Where 
		tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fechaRegistro < fechaIni_IN;
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ControlLDL` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ControlLDL`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;	
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ControlPTH` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ControlPTH`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_Creatinina` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_Creatinina`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)

)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstudiados, pacientesEstadios Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesEstudiados
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
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN;		
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		-1 pacientesControlados,
		(pacientesEstadios - pacientesEstudiados)  noMedidos,
		-1 vigentesControlados,
		-1 vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_HbA1c` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_HbA1c`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)

)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstudiados, pacientesEstadios, pacientesControlados Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesControlados
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
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)		
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN
		And tbl_cac.fecHemoGlicosilada > fechaFin;
		
	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.diagDiabetesMellitus = 1		
		And tbl_cac.fecHemoGlicosilada < fechaIni_IN;
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		pacientesControlados,
		(pacientesEstadios - pacientesEstudiados) noMedidos,
		pacientesControlados vigentesControlados,
		(pacientesEstadios - pacientesControlados) vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_HipertensionArterial` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_HipertensionArterial`(
	IN `fechaIni_IN` DATE,
	IN `idOrganizacion_IN` CHAR(36)



)
BEGIN

	Declare pacientesEstadios, pacientesEstudiados Int;
	
	Select
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.tensionArtSistolica < 140
		And tbl_cac.tensionArtDiastolica < 90
		And tbl_cac.fechaRegistro < fechaIni_IN;

	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		tbl_cac.diagDiabetesMellitus = 1
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fechaRegistro < fechaIni_IN;
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		-1 pacientesControlados,
		-1 noMedidos,
		pacientesEstudiados vigentesControlados,
		(pacientesEstadios - pacientesEstudiados) vigentesDescontrolados;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_LDL` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_LDL`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)


)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstadios, pacientesEstudiados, pacientesControlados Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesControlados
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.LDL <= 100
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaLDL < fechaIni_IN
		And tbl_cac.fecUltimaLDL > fechaFin;
		
	Select
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaLDL < fechaIni_IN
		And tbl_cac.fecUltimaLDL > fechaFin;

	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaLDL < fechaIni_IN;
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		pacientesControlados,
		(pacientesEstadios - pacientesEstudiados) noMedidos,
		pacientesControlados vigentesControlados,
		(pacientesEstadios - pacientesControlados) vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_MedicionAlbuminuria` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_MedicionAlbuminuria`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_MedicionCreatinina` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_MedicionCreatinina`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_MedicionHbA1c` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_MedicionHbA1c`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_MedicionLDL` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_MedicionLDL`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_MedicionPTH` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_MedicionPTH`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ProgresionEnfermedadRenal` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ProgresionEnfermedadRenal`(
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
		
	Select numerador, denominador, 
			IFNULL ((numerador / denominador), 0) as resultado;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_ProgresionEnfRenal` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_ProgresionEnfRenal`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)

)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstudiados, pacientesEstadios Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And tbl_cac.tasaFiltracionGlomerular < 5
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN
		And tbl_cac.fecUltimaCreatinina > fechaFin;

	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		(tbl_cac.estadioERC >= 1 AND tbl_cac.estadioERC <= 4)
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fecUltimaCreatinina < fechaIni_IN;
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		-1 pacientesControlados,
		-1 noMedidos,
		pacientesEstudiados vigentesControlados,
		(pacientesEstadios - pacientesEstudiados) vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Prc_PTH` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cactester`@`%` PROCEDURE `Prc_PTH`(
	IN `fechaIni_IN` DATE,
	IN `intervalo_IN` INT,
	IN `estadioERC_IN` INT,
	IN `PTHMayor_IN` INT,
	IN `PTHMenor_IN` INT,
	IN `idOrganizacion_IN` CHAR(36)

)
Begin

	Declare fechaFin DateTime;
	Declare pacientesEstudiados, pacientesEstadios, pacientesControlados Int;
	SET fechaFin = DATE_SUB(fechaIni_IN, INTERVAL intervalo_IN MONTH);
	
	Select
		Count(*) NumPacientes  Into pacientesControlados
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
		Count(*) NumPacientes  Into pacientesEstudiados
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1)
		And tbl_cac.fechaPTH < fechaIni_IN
		And tbl_cac.fechaPTH > fechaFin;

	Select
		Count(*) NumPacientes Into pacientesEstadios
	From
	 	tbl_cac
	Where 
		tbl_cac.estadioERC = estadioERC_IN
		And (tbl_cac.idOrganizacion = idOrganizacion_IN OR idOrganizacion_IN = -1);
		
	Select 
		pacientesEstadios, 
		pacientesEstudiados, 
		pacientesControlados,
		(pacientesEstadios - pacientesEstudiados) noMedidos,
		pacientesControlados vigentesControlados,
		(pacientesEstadios - pacientesControlados) vigentesDescontrolados;
	
End ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-08-02 10:02:15
