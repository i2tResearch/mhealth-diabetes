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
-- Table structure for table `tbl_archivo_cac`
--

DROP TABLE IF EXISTS `tbl_archivo_cac`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_archivo_cac` (
  `id` char(36) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `tamano` varchar(20) NOT NULL,
  `fechaCreacion` datetime NOT NULL,
  `numFilasImportadas` int(11) NOT NULL,
  `urlArchivo` varchar(500) NOT NULL,
  `idUsuario` char(36) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_archivo_cac_tbl_usuario` (`idUsuario`),
  CONSTRAINT `FK_tbl_archivo_cac_tbl_usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tbl_usuario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_archivo_cac`
--

LOCK TABLES `tbl_archivo_cac` WRITE;
/*!40000 ALTER TABLE `tbl_archivo_cac` DISABLE KEYS */;
INSERT INTO `tbl_archivo_cac` VALUES ('066ad990-1065-4523-83f5-f82d9c6df40c','temp.xlsx','9546','2017-05-24 21:23:11',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2F066ad990-1065-4523-83f5-f82d9c6df40c%2Ftemp.xlsx?alt=media&token=061be7cf-5577-478b-9513-5c1dbd00e581','7eafc423-fd52-4949-a791-4ce144783177'),('0b6544a3-904d-46e3-a447-eff2dba0bf2c','CONSOLIDADO, con archivos TotalesV2.zip','5220611','2017-06-21 11:31:12',0,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2F0b6544a3-904d-46e3-a447-eff2dba0bf2c%2FCONSOLIDADO%2C%20con%20archivos%20TotalesV2.zip?alt=media&token=66e4f948-e892-4408-944a-f1baf2d960a8','7eafc423-fd52-4949-a791-4ce144783177'),('1727a2ea-a81c-4558-9da1-013cd940ed51','temp.xlsx','9546','2017-05-24 15:23:56',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/793c99e5-a9d3-4e0d-8c22-70a852b88767%2F1727a2ea-a81c-4558-9da1-013cd940ed51%2Ftemp.xlsx?alt=media&token=ffefa3f9-a750-402c-bdbd-f52795ee4626','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('35fc6f10-f7e7-4d8e-a898-091a610be954','Preliminar Datos 10.xlsx','13461','2017-05-25 00:01:50',9,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2F35fc6f10-f7e7-4d8e-a898-091a610be954%2FPreliminar%20Datos%2010.xlsx?alt=media&token=c0ac294e-b36a-40db-8f4f-eef896d2c7af','7eafc423-fd52-4949-a791-4ce144783177'),('377e9d92-68de-4ba5-93c8-d07ae93f3959','temp.xlsx','9546','2017-05-24 15:29:42',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/793c99e5-a9d3-4e0d-8c22-70a852b88767%2F377e9d92-68de-4ba5-93c8-d07ae93f3959%2Ftemp.xlsx?alt=media&token=af9917c6-4787-422e-91cd-1e0aa75b0560','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('8267618e-526e-4b78-8ef3-8b90ca6157ab','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 18:51:25',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2F8267618e-526e-4b78-8ef3-8b90ca6157ab%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=a6a1aee3-5374-441d-980a-c054fdb63e27','7eafc423-fd52-4949-a791-4ce144783177'),('91120a33-a500-494e-a7d9-a5c269a97339','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 18:51:42',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2F91120a33-a500-494e-a7d9-a5c269a97339%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=a8185c6b-f862-4707-a892-ccff20bb805a','7eafc423-fd52-4949-a791-4ce144783177'),('939e2478-ce9c-4ac1-b555-451657d929e2','temp.xlsx','9546','2017-05-24 15:28:31',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/793c99e5-a9d3-4e0d-8c22-70a852b88767%2F939e2478-ce9c-4ac1-b555-451657d929e2%2Ftemp.xlsx?alt=media&token=f54e62d1-1aca-490b-bfe9-b95ce8e1bcff','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('a271931b-94c1-4270-b512-cd7671dd5b7c','Consolidado Tipo 300 Limpio.xlsx','215213','2017-06-16 11:13:37',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2Fa271931b-94c1-4270-b512-cd7671dd5b7c%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=ac67ad48-035b-4abd-ba47-997c581b5e2a','7eafc423-fd52-4949-a791-4ce144783177'),('a517c863-6b45-4f4d-b379-3cacfd6a6bf6','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 18:55:37',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2Fa517c863-6b45-4f4d-b379-3cacfd6a6bf6%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=22e601b3-4942-48c1-aa9b-39041d363673','7eafc423-fd52-4949-a791-4ce144783177'),('acdc4671-3b1c-45d5-993f-8d4a69709b9e','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 11:22:31',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2Facdc4671-3b1c-45d5-993f-8d4a69709b9e%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=dbb5648e-6f00-4ed1-8559-f0533f2513d6','7eafc423-fd52-4949-a791-4ce144783177'),('d9268c05-e74c-4cdd-aea4-6f093b9b5f9d','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 18:46:32',284,'NO_URL','7eafc423-fd52-4949-a791-4ce144783177'),('dde248cb-c84e-419c-a6dc-cc8f7b072d3a','CONSOLIDADO, con archivos TotalesV2.csv','14046420','2017-06-01 22:12:27',64292,'NO_URL','7eafc423-fd52-4949-a791-4ce144783177'),('e2345a2f-d1b9-4b98-8942-830ffcd8463b','CONSOLIDADO, con archivos TotalesV2.csv','28852504','2017-06-20 22:17:28',1000,'NO_URL','7eafc423-fd52-4949-a791-4ce144783177'),('e2813f0a-a496-4192-8139-23ccf896b3ea','temp.xlsx','9546','2017-05-24 15:16:27',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/793c99e5-a9d3-4e0d-8c22-70a852b88767%2Fe2813f0a-a496-4192-8139-23ccf896b3ea%2Ftemp.xlsx?alt=media&token=13837e34-56fe-4e82-907b-18f96733a47f','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('f01e204c-b702-434a-9a9f-24c97faec88d','Consolidado Tipo 300 Limpio.xlsx','220722','2017-06-16 18:51:34',284,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/7eafc423-fd52-4949-a791-4ce144783177%2Ff01e204c-b702-434a-9a9f-24c97faec88d%2FConsolidado%20Tipo%20300%20Limpio.xlsx?alt=media&token=82c1773e-82f2-473e-af61-aa31db651fc3','7eafc423-fd52-4949-a791-4ce144783177'),('f6414709-aee0-4261-9097-4eb5123d6c9a','temp.xlsx','9546','2017-05-24 15:26:24',1,'NO_URL','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('fdf55c04-5700-4560-b8b6-9ba26988e7cd','temp.xlsx','9546','2017-05-22 14:56:50',1,'https://firebasestorage.googleapis.com/v0/b/diabetesicesi-123d7.appspot.com/o/793c99e5-a9d3-4e0d-8c22-70a852b88767%2Ffdf55c04-5700-4560-b8b6-9ba26988e7cd%2Ftemp.xlsx?alt=media&token=45064d2e-ad30-4e18-a8d9-5ffa6ca2cf0e','793c99e5-a9d3-4e0d-8c22-70a852b88767');
/*!40000 ALTER TABLE `tbl_archivo_cac` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-08-02 10:02:00
