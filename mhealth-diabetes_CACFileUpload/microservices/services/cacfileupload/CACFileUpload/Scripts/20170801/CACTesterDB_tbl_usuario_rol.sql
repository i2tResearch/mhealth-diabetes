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
-- Table structure for table `tbl_usuario_rol`
--

DROP TABLE IF EXISTS `tbl_usuario_rol`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_usuario_rol` (
  `id` char(36) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `idRol` char(36) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  `idUsuario` char(36) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_tbl_usuario_rol_tbl_rol` (`idRol`),
  KEY `IX_FK_tbl_usuario_rol_tbl_usuario` (`idUsuario`),
  CONSTRAINT `FK_tbl_usuario_rol_tbl_rol` FOREIGN KEY (`idRol`) REFERENCES `tbl_rol` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tbl_usuario_rol_tbl_usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tbl_usuario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_usuario_rol`
--

LOCK TABLES `tbl_usuario_rol` WRITE;
/*!40000 ALTER TABLE `tbl_usuario_rol` DISABLE KEYS */;
INSERT INTO `tbl_usuario_rol` VALUES ('1093f6d7-b878-4070-a7ee-91df9aa6ec22','608c2786-efb0-11e6-bc64-92361f008978','793c99e5-a9d3-4e0d-8c22-70a852b88767'),('2d0100db-8c0f-4b00-9b77-31c979073154','608c2786-efb0-11e6-bc64-92361f008978','793c99e5-a9d3-4e0d-8c22-70a852b86598'),('eee2280f-2da1-4cd8-8cf0-ea9c910626b9','608c2786-efb0-11e6-bc64-92361f008978','7eafc423-fd52-4949-a791-4ce144783177');
/*!40000 ALTER TABLE `tbl_usuario_rol` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-08-02 10:02:07
