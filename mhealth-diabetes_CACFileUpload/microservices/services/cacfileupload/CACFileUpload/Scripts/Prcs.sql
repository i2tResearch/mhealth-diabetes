
-- Dumping structure for procedure mhealthdiabetesdb.Prc_ControlDiabetesMellitus
DELIMITER //
CREATE DEFINER=`root`@`%` PROCEDURE `Prc_ControlDiabetesMellitus`(
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
