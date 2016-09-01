-- phpMyAdmin SQL Dump
-- version 4.0.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 01, 2016 at 11:04 AM
-- Server version: 5.5.40-0ubuntu0.14.04.1
-- PHP Version: 5.5.9-1ubuntu4.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `unity`
--

-- --------------------------------------------------------

--
-- Table structure for table `configuraciones`
--

CREATE TABLE IF NOT EXISTS `configuraciones` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(250) NOT NULL,
  `valor` varchar(250) NOT NULL,
  `evento_id` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Dumping data for table `configuraciones`
--

INSERT INTO `configuraciones` (`id`, `nombre`, `valor`, `evento_id`) VALUES
(1, 'juego_activo', '1', 6),
(2, 'juego_nombre', 'jumper', 6),
(3, 'memotest_tiempo', '60', 6),
(4, 'memotest_grupo_id', '3', 6),
(5, 'trivia_grupo_id', '4', 6),
(6, 'premios_activos', '1', 6),
(7, 'cantidad_premios', '3', 6),
(8, 'formulario_activo', '1', 6),
(9, 'header_banco_id', '1', 6);

-- --------------------------------------------------------

--
-- Table structure for table `eventos`
--

CREATE TABLE IF NOT EXISTS `eventos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(255) NOT NULL,
  `nombre` varchar(255) NOT NULL,
  `ciudad` varchar(255) NOT NULL,
  `fecha_desde` datetime DEFAULT NULL,
  `fecha_hasta` datetime DEFAULT NULL,
  `created` datetime NOT NULL,
  `modified` datetime DEFAULT NULL,
  `deleted` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Dumping data for table `eventos`
--

INSERT INTO `eventos` (`id`, `code`, `nombre`, `ciudad`, `fecha_desde`, `fecha_hasta`, `created`, `modified`, `deleted`) VALUES
(1, 'test', 'Test', 'Test', NULL, NULL, '2015-07-22 18:23:44', '2015-10-29 20:09:46', '2015-10-29 08:09:46'),
(2, 'test2', 'Test2', 'Test2', NULL, NULL, '2015-07-22 18:28:04', '2015-10-29 20:12:34', '2015-10-29 08:12:34'),
(3, 'code1234', 'Evento Test', 'Buenos Aires', NULL, NULL, '2015-07-23 00:00:00', '2015-10-29 20:12:33', '2015-10-29 08:12:33'),
(4, 'coode', 'Test3', 'city', NULL, NULL, '2015-08-06 14:55:49', '2015-10-29 20:12:31', '2015-10-29 08:12:31'),
(5, 'asdfqwer', 'test5', 'asdf', NULL, NULL, '2015-08-06 14:58:57', '2015-10-29 20:08:12', '2015-10-29 08:08:12'),
(6, 'CODE12345', 'Nombre Nuevo Evento', 'Buenos Aires', NULL, NULL, '2015-10-29 20:06:19', NULL, NULL),
(7, 'CODETEST', 'NombreTest', 'CiudadTest', NULL, NULL, '2015-10-29 20:11:05', '2015-10-29 20:12:30', '2015-10-29 08:12:30'),
(8, 'CODETEST2', 'NombreTest', 'CiudadTest', NULL, NULL, '2015-10-29 20:11:34', '2015-10-29 20:12:28', '2015-10-29 08:12:28');

-- --------------------------------------------------------

--
-- Table structure for table `participantes`
--

CREATE TABLE IF NOT EXISTS `participantes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `evento_id` int(11) NOT NULL,
  `nombre` blob NOT NULL,
  `apellido` blob NOT NULL,
  `email` blob NOT NULL,
  `telefono` blob NOT NULL,
  `dni` blob NOT NULL,
  `provincia` blob NOT NULL,
  `acepta_beneficios` tinyint(1) NOT NULL,
  `promotora_id` int(11) NOT NULL,
  `fecha_ingresado` datetime NOT NULL,
  `created` datetime NOT NULL,
  `juego_nombre` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `participantes`
--

INSERT INTO `participantes` (`id`, `evento_id`, `nombre`, `apellido`, `email`, `telefono`, `dni`, `provincia`, `acepta_beneficios`, `promotora_id`, `fecha_ingresado`, `created`, `juego_nombre`) VALUES
(1, 6, 0x356463643961323565656261613939393430633538313266663961353463343761643966336438643862316536316536356436393638663638393535336663318d931aea73f0c297b9d683b54a0595b8c3578ac2460d69191617303bd34f6afc, 0x393164333462643063666465333031613436656261356634633262653866306461663933363633346534386266366234393739353131666235643863613838621f4786dd00dd790617b2ef4f76734b1b3fddbd44d71076cb5ea8cf3e4fa6871a, 0x6233386639666137376533393439633737323737373664616661323138333234636663356333613062323431343734353663643764313936346332323734363100be640f99876c76431332f51e87b0d3ead30d6d1a8189aa46af2d9ffac7b381, 0x626535336630313031623836613936653832393136653365356636353132353562323630353637386563623830393238373736356130616666316435323165635d8a2e397b9b4543a2a26f1521ff9c75f0c3fc665554efa3f472c47aff582faf, 0x30396635326636353039323263656536343538356264353233316432613030313330303463656530356538323831393235623835323135633433613531333038cc573ad5fe2002140e10a5e109fe1e98e883182d9cf3ff9bb3c09caf9e200175, 0x303632653833663763333338316138653433366235306531336264396166363361363962306237633366326165613232663163643134313839363133313737394f677851b242cf5ddc6bc2b6e52dfba8ee813e5b35daf973cbf3c6f961582aaf, 1, 1, '0000-00-00 00:00:00', '2015-11-05 12:33:31', 'Trivia');

-- --------------------------------------------------------

--
-- Table structure for table `premios`
--

CREATE TABLE IF NOT EXISTS `premios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `participante_id` int(11) NOT NULL,
  `fecha_entregado` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `respuestas_participantes`
--

CREATE TABLE IF NOT EXISTS `respuestas_participantes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `participante_id` int(11) NOT NULL,
  `pregunta_id` int(11) NOT NULL,
  `respuesta_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tablets`
--

CREATE TABLE IF NOT EXISTS `tablets` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(250) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `usuarios`
--

CREATE TABLE IF NOT EXISTS `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(250) NOT NULL,
  `password` varchar(250) NOT NULL,
  `nombre` varchar(250) NOT NULL,
  `apellido` varchar(250) NOT NULL,
  `admin` tinyint(1) NOT NULL,
  `created` datetime NOT NULL,
  `modified` datetime DEFAULT NULL,
  `deleted` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `usuarios`
--

INSERT INTO `usuarios` (`id`, `username`, `password`, `nombre`, `apellido`, `admin`, `created`, `modified`, `deleted`) VALUES
(1, 'admin', '6cee4dfaca15f9f7ba7b577a652f9a96', 'NombreAdmin', 'ApellidoAdmin', 1, '0000-00-00 00:00:00', '2015-10-29 19:40:03', NULL),
(2, 'promotora', '6cee4dfaca15f9f7ba7b577a652f9a96', 'NombrePromotora', 'ApellidoPromotora', 0, '0000-00-00 00:00:00', '2015-10-29 19:40:46', NULL);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
