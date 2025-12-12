-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 12-12-2025 a las 07:52:53
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistema_tienda_mascotas`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categoriasproducto`
--

CREATE TABLE `categoriasproducto` (
  `IdCategoria` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Descripcion` varchar(200) DEFAULT NULL,
  `Activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `categoriasproducto`
--

INSERT INTO `categoriasproducto` (`IdCategoria`, `Nombre`, `Descripcion`, `Activo`) VALUES
(1, 'Alimentos', 'Comida para mascotas', 1),
(2, 'Juguetes', 'Juguetes para entretenimiento', 1),
(3, 'Accesorios', 'Collares, correas, etc.', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `IdCliente` int(11) NOT NULL,
  `Nombres` varchar(100) NOT NULL,
  `Apellidos` varchar(100) NOT NULL,
  `Documento` varchar(20) DEFAULT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Direccion` varchar(200) DEFAULT NULL,
  `Activo` tinyint(1) NOT NULL DEFAULT 1,
  `FechaRegistro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`IdCliente`, `Nombres`, `Apellidos`, `Documento`, `Telefono`, `Email`, `Direccion`, `Activo`, `FechaRegistro`) VALUES
(1, 'Juan', 'Pérez', '0102030405', '099111111', 'juan.perez@example.com', 'Av. Siempre Viva 123', 1, '2025-12-11 20:55:29'),
(2, 'María', 'López', '0203040506', '099222222', 'maria.lopez@example.com', 'Calle Los Álamos 456', 1, '2025-12-11 20:55:29'),
(3, 'Carlos', 'Ruiz', '0304050607', '099333333', 'carlos.ruiz@example.com', 'Av. Central 789', 1, '2025-12-11 20:55:29'),
(4, 'Mateo Alejandro', 'Cevallos Chávez', '2300982349', '1234567890', 'matcev04@outlook.com', 'Av Benito', 1, '2025-12-12 01:30:38');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventa`
--

CREATE TABLE `detalleventa` (
  `IdDetalle` int(11) NOT NULL,
  `IdVenta` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL,
  `PrecioUnitario` decimal(10,2) NOT NULL,
  `Subtotal` decimal(10,2) GENERATED ALWAYS AS (`Cantidad` * `PrecioUnitario`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `detalleventa`
--

INSERT INTO `detalleventa` (`IdDetalle`, `IdVenta`, `IdProducto`, `Cantidad`, `PrecioUnitario`) VALUES
(1, 1, 1, 2, 15.50),
(2, 2, 2, 1, 13.25),
(3, 3, 3, 3, 7.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `IdMascota` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Especie` varchar(50) NOT NULL,
  `Raza` varchar(100) DEFAULT NULL,
  `FechaNacimiento` date DEFAULT NULL,
  `Activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `mascotas`
--

INSERT INTO `mascotas` (`IdMascota`, `IdCliente`, `Nombre`, `Especie`, `Raza`, `FechaNacimiento`, `Activo`) VALUES
(1, 1, 'Rocky', 'Perro', 'Labrador', '2020-05-10', 1),
(2, 2, 'Michi', 'Gato', 'Siames', '2021-03-15', 1),
(3, 3, 'Zeus', 'Perro', 'Pastor Alemán', '2019-11-20', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `IdProducto` int(11) NOT NULL,
  `IdCategoria` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Descripcion` varchar(200) DEFAULT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `Stock` int(11) NOT NULL DEFAULT 0,
  `Activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`IdProducto`, `IdCategoria`, `Nombre`, `Descripcion`, `Precio`, `Stock`, `Activo`) VALUES
(1, 1, 'Alimento Perro 2kg', 'Bolsa de alimento seco para perro', 15.50, 50, 1),
(2, 1, 'Alimento Gato 1.5kg', 'Bolsa de alimento seco para gato', 13.25, 40, 1),
(3, 2, 'Pelota para perro', 'Pelota de goma resistente', 7.00, 30, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `IdVenta` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `Fecha` datetime NOT NULL DEFAULT current_timestamp(),
  `Total` decimal(10,2) NOT NULL DEFAULT 0.00,
  `Estado` varchar(20) NOT NULL DEFAULT 'Registrada'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`IdVenta`, `IdCliente`, `Fecha`, `Total`, `Estado`) VALUES
(1, 1, '2025-01-10 10:30:00', 31.00, 'Registrada'),
(2, 2, '2025-01-11 16:45:00', 13.25, 'Registrada'),
(3, 3, '2025-01-12 11:20:00', 21.00, 'Registrada');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `categoriasproducto`
--
ALTER TABLE `categoriasproducto`
  ADD PRIMARY KEY (`IdCategoria`);

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`IdCliente`);

--
-- Indices de la tabla `detalleventa`
--
ALTER TABLE `detalleventa`
  ADD PRIMARY KEY (`IdDetalle`),
  ADD KEY `FK_DetalleVenta_Ventas` (`IdVenta`),
  ADD KEY `FK_DetalleVenta_Productos` (`IdProducto`);

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`IdMascota`),
  ADD KEY `FK_Mascotas_Clientes` (`IdCliente`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`IdProducto`),
  ADD KEY `FK_Productos_Categorias` (`IdCategoria`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`IdVenta`),
  ADD KEY `FK_Ventas_Clientes` (`IdCliente`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `categoriasproducto`
--
ALTER TABLE `categoriasproducto`
  MODIFY `IdCategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `IdCliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `detalleventa`
--
ALTER TABLE `detalleventa`
  MODIFY `IdDetalle` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `IdMascota` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `IdProducto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `ventas`
--
ALTER TABLE `ventas`
  MODIFY `IdVenta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detalleventa`
--
ALTER TABLE `detalleventa`
  ADD CONSTRAINT `FK_DetalleVenta_Productos` FOREIGN KEY (`IdProducto`) REFERENCES `productos` (`IdProducto`),
  ADD CONSTRAINT `FK_DetalleVenta_Ventas` FOREIGN KEY (`IdVenta`) REFERENCES `ventas` (`IdVenta`);

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `FK_Mascotas_Clientes` FOREIGN KEY (`IdCliente`) REFERENCES `clientes` (`IdCliente`);

--
-- Filtros para la tabla `productos`
--
ALTER TABLE `productos`
  ADD CONSTRAINT `FK_Productos_Categorias` FOREIGN KEY (`IdCategoria`) REFERENCES `categoriasproducto` (`IdCategoria`);

--
-- Filtros para la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD CONSTRAINT `FK_Ventas_Clientes` FOREIGN KEY (`IdCliente`) REFERENCES `clientes` (`IdCliente`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
