CREATE DATABASE  IF NOT EXISTS `facebook_lite` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `facebook_lite`;
-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: localhost    Database: facebook_lite
-- ------------------------------------------------------
-- Server version	8.0.29

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `conversations`
--

DROP TABLE IF EXISTS `conversations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `conversations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date_created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `creator_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `creator_id` (`creator_id`),
  CONSTRAINT `conversations_ibfk_1` FOREIGN KEY (`creator_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conversations`
--

LOCK TABLES `conversations` WRITE;
/*!40000 ALTER TABLE `conversations` DISABLE KEYS */;
INSERT INTO `conversations` VALUES (49,'2022-12-26 23:17:17',30),(50,'2022-12-26 23:17:52',30),(51,'2022-12-26 23:18:16',30),(52,'2022-12-28 23:59:19',38),(53,'2022-12-29 20:33:15',30),(54,'2022-12-29 22:18:52',39),(55,'2022-12-30 00:55:17',40),(56,'2022-12-30 23:41:13',30),(57,'2022-12-30 23:44:48',30),(58,'2022-12-30 23:45:13',30),(59,'2023-01-04 11:21:22',30),(60,'2023-01-04 11:21:40',30),(61,'2023-01-04 11:46:08',39),(62,'2023-01-04 11:46:47',34),(63,'2023-01-04 11:47:27',42),(64,'2023-01-04 14:03:25',42),(65,'2023-01-04 14:04:58',42);
/*!40000 ALTER TABLE `conversations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messages`
--

DROP TABLE IF EXISTS `messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `messages` (
  `id` int NOT NULL AUTO_INCREMENT,
  `content` mediumtext,
  `date_created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sender_id` int DEFAULT NULL,
  `conversations_id` int DEFAULT NULL,
  `is_visible` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `sender_id` (`sender_id`),
  KEY `conversations_id` (`conversations_id`),
  CONSTRAINT `messages_ibfk_3` FOREIGN KEY (`sender_id`) REFERENCES `users` (`id`),
  CONSTRAINT `messages_ibfk_4` FOREIGN KEY (`conversations_id`) REFERENCES `conversations` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=123 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messages`
--

LOCK TABLES `messages` WRITE;
/*!40000 ALTER TABLE `messages` DISABLE KEYS */;
INSERT INTO `messages` VALUES (93,'Hej Rhonda! :)','2022-12-26 23:17:22',30,49,1),(94,'Hej Elina :)','2022-12-26 23:17:58',30,50,1),(95,'Hej allihopa :)','2022-12-26 23:18:22',30,51,1),(96,'Hej angie :) :)','2022-12-26 23:26:51',38,49,1),(97,'Hej anneli! :)','2022-12-28 23:59:22',38,52,1),(98,'Hallå?','2022-12-29 20:19:56',30,51,1),(99,'Hello?','2022-12-29 20:20:02',30,50,1),(100,'Vad gör du?','2022-12-29 20:20:07',30,49,1),(101,'Hej Daniel :)','2022-12-29 20:33:19',30,53,1),(102,'Hej! Jag har skickat en vänförfrågan :)','2022-12-29 22:19:00',39,54,1),(103,'Åh vad kul tack :)','2022-12-29 22:19:23',30,54,1),(104,'Hej Svaaaan!','2022-12-30 00:55:21',40,55,1),(105,'Finns ingen returknapp här hehe så måste skriva','2022-12-30 00:56:10',40,55,1),(106,'Hej Stella! Long time no seen','2022-12-30 00:56:44',33,55,1),(107,'?','2022-12-30 23:23:43',30,49,1),(108,'Hello','2023-01-02 23:37:56',30,50,1),(109,'Hello','2023-01-02 23:38:04',30,51,1),(110,'Hej :))','2023-01-02 23:57:52',32,50,1),(111,'Hej','2023-01-03 16:45:15',30,50,1),(112,'h','2023-01-03 19:17:44',30,49,1),(113,'kk','2023-01-03 19:17:48',30,50,1),(114,'jj','2023-01-03 19:17:52',30,51,1),(115,'k','2023-01-03 19:17:55',30,51,1),(116,'gdh','2023-01-03 19:18:00',30,57,1),(117,'jsjs','2023-01-04 11:21:04',30,50,1),(118,'Hej','2023-01-04 11:21:49',30,60,1),(119,'Hej Anna :) Hur är läget?','2023-01-04 11:46:19',39,61,1),(120,'Heeeeej Anna! Kul att se dig här på fb :)','2023-01-04 11:47:01',34,62,1),(121,'Hej Alla kollegor!! Här kan vi planera våra träffar =)','2023-01-04 11:47:48',42,63,1),(122,'Hello alla :)','2023-01-04 14:05:08',42,65,1);
/*!40000 ALTER TABLE `messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `content` text,
  `date_created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `users_id` int DEFAULT NULL,
  `posts_types_id` int DEFAULT NULL,
  `on_post_id` int DEFAULT NULL,
  `is_visible` tinyint(1) NOT NULL DEFAULT '1',
  `is_edited` tinyint(1) NOT NULL DEFAULT '0',
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `users_id` (`users_id`),
  KEY `posts_types_id` (`posts_types_id`),
  KEY `on_post_id` (`on_post_id`),
  CONSTRAINT `posts_ibfk_1` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`),
  CONSTRAINT `posts_ibfk_2` FOREIGN KEY (`posts_types_id`) REFERENCES `posts_types` (`id`),
  CONSTRAINT `posts_ibfk_3` FOREIGN KEY (`on_post_id`) REFERENCES `posts` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts`
--

LOCK TABLES `posts` WRITE;
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
INSERT INTO `posts` VALUES (41,'Kollar walking dead och dricker cola <3','2022-12-26 18:58:03',30,1,NULL,1,0,0),(42,'Jag har badat idag ochd et va skönt','2022-12-29 14:26:02',32,1,NULL,1,0,0),(43,'Pluggdag!','2022-12-29 20:34:36',30,1,NULL,1,0,1),(44,'Åh vad härligt :)','2022-12-29 20:36:53',38,2,42,1,0,0),(45,'My cat is the best cat <3333','2022-12-30 00:55:45',40,1,NULL,1,0,0),(46,'Same same! :)','2022-12-30 01:04:35',41,2,43,1,0,0),(47,'Finns ingen returknapp så får visst skriva något :)','2022-12-30 11:26:48',30,1,NULL,1,0,0),(48,'Ett inlägg :)','2022-12-30 23:23:23',30,1,NULL,1,0,0),(49,'Fått nya glasögon idag8)','2023-01-02 23:36:24',30,1,NULL,1,0,0),(50,'Åh va dhrägit','2023-01-03 16:45:29',30,2,42,1,0,0),(51,'d','2023-01-03 16:45:40',30,2,42,1,0,0),(52,'Åh vilket vinterlandskap vi vakna till idag <3333','2023-01-04 11:41:55',42,1,NULL,1,0,0);
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts_types`
--

DROP TABLE IF EXISTS `posts_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts_types` (
  `id` int NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts_types`
--

LOCK TABLES `posts_types` WRITE;
/*!40000 ALTER TABLE `posts_types` DISABLE KEYS */;
INSERT INTO `posts_types` VALUES (1,'wall_post'),(2,'comment');
/*!40000 ALTER TABLE `posts_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `id` int NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Super Admin'),(2,'Administrator'),(3,'Editor'),(4,'Customer Service'),(5,'Member');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(80) DEFAULT NULL,
  `last_name` varchar(80) DEFAULT NULL,
  `email` varchar(80) NOT NULL,
  `pass_word` varchar(20) NOT NULL,
  `birth_date` date DEFAULT NULL,
  `gender` varchar(20) DEFAULT NULL,
  `about_me` varchar(200) DEFAULT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `date_inactive` datetime DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  `role_id` int DEFAULT NULL,
  `account_created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (30,'Angelina','Holmqvist','angelina@live.se','Hej123','1994-03-14','Woman',NULL,1,NULL,0,5,'2022-01-02 20:42:25'),(31,'Karen','Joseph','karen@live.com','Hej123','1957-07-03','Man','A real Karen and I will report your page.',1,NULL,0,5,'2021-08-12 06:23:25'),(32,'ELINA','Kerola','elina@live.se','Hej123','1990-03-17','Woman','William<3 Alva<3.',1,NULL,0,5,'2022-01-02 20:42:25'),(33,'Daniel','Svan','daniel@live.com','Hej123','1995-04-11','Man','Bella<333',1,NULL,0,5,'2023-01-03 20:42:25'),(34,'Anneli','Svan','anneli@live.com','Hej123','1967-06-21','Woman','',1,NULL,0,5,'2023-01-03 20:42:25'),(35,'Agneta','Holmqvist','agneta@live.se','Hej123','1953-10-02','Woman','',1,NULL,0,5,'2021-08-12 06:23:25'),(36,'Eric','Dixon','eric@live.se','Hej123','2002-10-21','Man','Carpe diem',1,NULL,0,5,'2023-01-03 20:42:25'),(37,'Alice','Carlstein','alice@live.se','Hej123','2000-01-01','Woman','Millenials<33FTW',1,NULL,0,5,'2023-01-03 20:42:25'),(38,'Rhonda','Marks','rhonda@live.se','Hej123','1993-05-01','Woman','',1,NULL,0,5,'2022-01-02 20:42:25'),(39,'Bella','Svan','bella@live.se','Hej123','1994-03-14','Woman',NULL,1,NULL,0,5,'2023-01-03 20:42:25'),(40,'Stella','Björne','stella@live.se','Hej123','1994-02-12','Agender',NULL,1,NULL,0,5,'2021-08-12 06:23:25'),(41,'Olle','Stensson','olle@live.se','Hej123','1993-02-02','Bigender',NULL,1,NULL,0,5,'2023-01-03 20:42:25'),(42,'Anna','Karlsson','anna@live.se','Hej123','1990-09-09','Bigender','Glada Anna här =)))',1,NULL,0,5,'2023-01-04 11:40:30');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_blocked`
--

DROP TABLE IF EXISTS `users_blocked`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users_blocked` (
  `users_id` int DEFAULT NULL,
  `blocked_user_id` int DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`),
  KEY `users_id` (`users_id`),
  KEY `blocked_user_id` (`blocked_user_id`),
  CONSTRAINT `users_blocked_ibfk_1` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`),
  CONSTRAINT `users_blocked_ibfk_2` FOREIGN KEY (`blocked_user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_blocked`
--

LOCK TABLES `users_blocked` WRITE;
/*!40000 ALTER TABLE `users_blocked` DISABLE KEYS */;
INSERT INTO `users_blocked` VALUES (42,40,11);
/*!40000 ALTER TABLE `users_blocked` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_conversations`
--

DROP TABLE IF EXISTS `users_conversations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users_conversations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `users_id` int DEFAULT NULL,
  `conversations_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `users_id` (`users_id`),
  KEY `conversations_id` (`conversations_id`),
  CONSTRAINT `users_conversations_ibfk_1` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`),
  CONSTRAINT `users_conversations_ibfk_2` FOREIGN KEY (`conversations_id`) REFERENCES `conversations` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=137 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_conversations`
--

LOCK TABLES `users_conversations` WRITE;
/*!40000 ALTER TABLE `users_conversations` DISABLE KEYS */;
INSERT INTO `users_conversations` VALUES (98,38,49),(99,30,49),(100,32,50),(101,30,50),(102,35,51),(103,34,51),(104,30,51),(105,34,52),(106,38,52),(107,33,53),(108,30,53),(109,30,54),(110,39,54),(111,33,55),(112,40,55),(113,33,56),(114,31,57),(115,30,57),(116,31,58),(117,36,58),(118,30,58),(119,32,59),(120,30,59),(121,36,60),(122,30,60),(123,42,61),(124,39,61),(125,42,62),(126,34,62),(127,41,63),(128,32,63),(129,42,63),(130,38,64),(131,31,64),(132,41,64),(133,42,64),(134,33,65),(135,37,65),(136,42,65);
/*!40000 ALTER TABLE `users_conversations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users_friends`
--

DROP TABLE IF EXISTS `users_friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users_friends` (
  `users_id1` int DEFAULT NULL,
  `users_id2` int DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `is_accepted` tinyint(1) NOT NULL DEFAULT '0',
  `date_friended` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `users_id1` (`users_id1`),
  KEY `users_id2` (`users_id2`),
  CONSTRAINT `users_friends_ibfk_1` FOREIGN KEY (`users_id1`) REFERENCES `users` (`id`),
  CONSTRAINT `users_friends_ibfk_2` FOREIGN KEY (`users_id2`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_friends`
--

LOCK TABLES `users_friends` WRITE;
/*!40000 ALTER TABLE `users_friends` DISABLE KEYS */;
INSERT INTO `users_friends` VALUES (38,39,59,0,NULL),(36,30,60,1,NULL),(30,36,61,1,NULL),(42,31,62,0,NULL),(42,32,63,1,NULL),(42,38,64,0,NULL),(38,42,65,0,NULL),(38,42,66,0,NULL),(38,42,67,0,NULL),(38,42,68,0,NULL),(38,42,69,0,NULL),(32,42,70,1,NULL),(42,36,71,1,NULL),(36,42,72,1,NULL),(42,34,73,1,NULL),(42,39,74,1,NULL),(39,42,75,1,NULL),(34,42,76,1,NULL),(38,42,77,0,NULL);
/*!40000 ALTER TABLE `users_friends` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-10 14:28:01
