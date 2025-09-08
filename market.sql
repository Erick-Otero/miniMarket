-- CREACIÓN DE BASE DE DATOS
CREATE DATABASE market;
GO

USE market;
GO

/* ========================
   TABLAS DE PRODUCCIÓN
   ======================== */

-- Categorías
CREATE TABLE categorias (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre_categoria NVARCHAR(100) NOT NULL
);

-- Marcas
CREATE TABLE marcas (
    id_marca INT IDENTITY(1,1) PRIMARY KEY,
    nombre_marca NVARCHAR(100) NOT NULL
);

-- Productos
CREATE TABLE productos (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre_producto NVARCHAR(150) NOT NULL,
    id_marca INT NOT NULL,
    id_categoria INT NOT NULL,
    anio_modelo INT NOT NULL,
    precio_lista DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_marca) REFERENCES marcas(id_marca),
    FOREIGN KEY (id_categoria) REFERENCES categorias(id_categoria)
);

-- Inventario
CREATE TABLE inventario (
    id_tienda INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    PRIMARY KEY (id_tienda, id_producto),
    FOREIGN KEY (id_producto) REFERENCES productos(id_producto)
);

/* ========================
   TABLAS DE VENTAS
   ======================== */

-- Clientes
CREATE TABLE clientes (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20),
    correo NVARCHAR(100),
    calle NVARCHAR(200),
    ciudad NVARCHAR(100),
    estado NVARCHAR(100),
    codigo_postal NVARCHAR(20)
);

-- Tiendas
CREATE TABLE tiendas (
    id_tienda INT IDENTITY(1,1) PRIMARY KEY,
    nombre_tienda NVARCHAR(150) NOT NULL,
    telefono NVARCHAR(20),
    correo NVARCHAR(100),
    calle NVARCHAR(200),
    ciudad NVARCHAR(100),
    estado NVARCHAR(100),
    codigo_postal NVARCHAR(20)
);

-- Empleados
CREATE TABLE empleados (
    id_empleado INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    correo NVARCHAR(100),
    telefono NVARCHAR(20),
    activo BIT NOT NULL,
    id_tienda INT NOT NULL,
    id_jefe INT NULL,
    contrasena NVARCHAR(200) NOT NULL,
    FOREIGN KEY (id_tienda) REFERENCES tiendas(id_tienda),
    FOREIGN KEY (id_jefe) REFERENCES empleados(id_empleado)
);

-- Órdenes
CREATE TABLE ordenes (
    id_orden INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT NOT NULL,
    estado_orden NVARCHAR(20),
    fecha_orden DATE NOT NULL,
    fecha_requerida DATE,
    fecha_envio DATE,
    id_tienda INT NOT NULL,
    id_empleado INT NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente),
    FOREIGN KEY (id_tienda) REFERENCES tiendas(id_tienda),
    FOREIGN KEY (id_empleado) REFERENCES empleados(id_empleado)
);

-- Detalle de Órdenes
CREATE TABLE detalle_ordenes (
    id_orden INT NOT NULL,
    id_item INT IDENTITY(1,1) NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    precio_lista DECIMAL(10,2) NOT NULL,
    descuento DECIMAL(4,2) NOT NULL,
    PRIMARY KEY (id_orden, id_item),
    FOREIGN KEY (id_orden) REFERENCES ordenes(id_orden),
    FOREIGN KEY (id_producto) REFERENCES productos(id_producto)
);

-- Relación Inventario con Tiendas
ALTER TABLE inventario
ADD FOREIGN KEY (id_tienda) REFERENCES tiendas(id_tienda);

/* ========================
   DATOS DE PRODUCCIÓN
   ======================== */

-- Categorías
INSERT INTO categorias (nombre_categoria) VALUES
('Lácteos'),
('Bebidas'),
('Snacks'),
('Carnes'),
('Panadería'),
('Frutas'),
('Verduras'),
('Aseo personal'),
('Limpieza hogar'),
('Tecnología');

-- Marcas
INSERT INTO marcas (nombre_marca) VALUES
('Nestlé'),
('Coca-Cola'),
('Pepsi'),
('Bimbo'),
('Colgate'),
('Samsung'),
('LG'),
('Procter & Gamble'),
('Apple'),
('Sony');

-- Productos
INSERT INTO productos (nombre_producto, id_marca, id_categoria, anio_modelo, precio_lista) VALUES
('Leche Entera', 1, 1, 2024, 1.50),
('Coca-Cola 1L', 2, 2, 2024, 0.99),
('Pepsi 1L', 3, 2, 2024, 0.95),
('Pan Blanco', 4, 5, 2024, 1.20),
('Cepillo Dental', 5, 8, 2024, 2.50),
('Smartphone Galaxy S24', 6, 10, 2024, 899.99),
('Televisor LG 55"', 7, 10, 2024, 599.99),
('Detergente Ariel', 8, 9, 2024, 5.99),
('iPhone 15', 9, 10, 2024, 999.99),
('Audífonos Sony WH-1000XM5', 10, 10, 2024, 349.99);

-- Tiendas
INSERT INTO tiendas (nombre_tienda, telefono, correo, calle, ciudad, estado, codigo_postal) VALUES
('SuperMarket Centro', '2222-1111', 'centro@market.com', 'Av. Central 123', 'San Salvador', 'San Salvador', '1101'),
('SuperMarket Norte', '2222-2222', 'norte@market.com', 'Calle Norte 45', 'San Salvador', 'San Salvador', '1102'),
('SuperMarket Sur', '2222-3333', 'sur@market.com', 'Boulevard Sur 98', 'Santa Tecla', 'La Libertad', '1501'),
('SuperMarket Este', '2222-4444', 'este@market.com', 'Carretera Este 76', 'Soyapango', 'San Salvador', '1201'),
('SuperMarket Oeste', '2222-5555', 'oeste@market.com', 'Av. Oeste 12', 'Mejicanos', 'San Salvador', '1301'),
('Market Premium', '2222-6666', 'premium@market.com', 'Zona Rosa 321', 'San Salvador', 'San Salvador', '1103'),
('MiniMarket 1', '2222-7777', 'mini1@market.com', 'Colonia Escalón 45', 'San Salvador', 'San Salvador', '1104'),
('MiniMarket 2', '2222-8888', 'mini2@market.com', 'Colonia Miramonte 12', 'San Salvador', 'San Salvador', '1105'),
('Market Express', '2222-9999', 'express@market.com', 'Metrocentro Local 12', 'San Salvador', 'San Salvador', '1106'),
('Market Digital', '2233-0000', 'digital@market.com', 'Centro Comercial La Gran Vía', 'Antiguo Cuscatlán', 'La Libertad', '1502');

-- Inventario
INSERT INTO inventario (id_tienda, id_producto, cantidad) VALUES
(1,1,100), (1,2,200), (1,3,150), (1,4,300), (1,5,50),
(2,6,20), (2,7,15), (2,8,100), (2,9,10), (2,10,25);

 /* ========================
   DATOS DE VENTAS
   ======================== */

-- Clientes
INSERT INTO clientes (nombre, apellido, telefono, correo, calle, ciudad, estado, codigo_postal) VALUES
('Carlos','Hernández','7777-1111','carlos.h@correo.com','Col. Escalón 23','San Salvador','San Salvador','1101'),
('Ana','Martínez','7777-2222','ana.m@correo.com','Av. Los Héroes 45','San Salvador','San Salvador','1102'),
('Luis','Gómez','7777-3333','luis.g@correo.com','Col. Miramonte 78','San Salvador','San Salvador','1103'),
('Marta','López','7777-4444','marta.l@correo.com','Calle Chiltiupán 12','Santa Tecla','La Libertad','1501'),
('José','Ramírez','7777-5555','jose.r@correo.com','Av. Roosevelt 90','San Salvador','San Salvador','1104'),
('María','Pérez','7777-6666','maria.p@correo.com','Boulevard Constitución 34','San Salvador','San Salvador','1105'),
('Ricardo','Flores','7777-7777','ricardo.f@correo.com','Col. San Benito 14','San Salvador','San Salvador','1106'),
('Carmen','Sánchez','7777-8888','carmen.s@correo.com','Av. Independencia 23','Soyapango','San Salvador','1201'),
('Jorge','Castro','7777-9999','jorge.c@correo.com','Col. Altamira 56','San Salvador','San Salvador','1107'),
('Laura','Morales','7777-0000','laura.m@correo.com','Col. Escalón 99','San Salvador','San Salvador','1108');

-- Empleados
INSERT INTO empleados (nombre, apellido, correo, telefono, activo, id_tienda, id_jefe, contrasena) VALUES
('Pedro','Alvarado','pedro.a@market.com','2223-1111',1,1,NULL,'1234'),
('Lucía','Hernández','lucia.h@market.com','2223-2222',1,1,1,'1234'),
('Javier','Martínez','javier.m@market.com','2223-3333',1,2,NULL,'1234'),
('Rosa','García','rosa.g@market.com','2223-4444',1,2,3,'1234'),
('Andrés','López','andres.l@market.com','2223-5555',1,3,NULL,'1234'),
('Elena','Moreno','elena.m@market.com','2223-6666',1,3,5,'1234'),
('Sofía','Castillo','sofia.c@market.com','2223-7777',1,4,NULL,'1234'),
('Mario','Díaz','mario.d@market.com','2223-8888',1,4,7,'1234'),
('Paola','Torres','paola.t@market.com','2223-9999',1,5,NULL,'1234'),
('Hugo','Reyes','hugo.r@market.com','2223-0000',1,5,9,'1234');

-- Órdenes
INSERT INTO ordenes (id_cliente, estado_orden, fecha_orden, fecha_requerida, fecha_envio, id_tienda, id_empleado) VALUES
(1,'Pendiente','2024-08-01','2024-08-05',NULL,1,1),
(2,'Completada','2024-08-02','2024-08-06','2024-08-05',1,2),
(3,'Pendiente','2024-08-03','2024-08-07',NULL,2,3),
(4,'Cancelada','2024-08-04','2024-08-08',NULL,2,4),
(5,'Pendiente','2024-08-05','2024-08-09',NULL,3,5),
(6,'Completada','2024-08-06','2024-08-10','2024-08-08',3,6),
(7,'Pendiente','2024-08-07','2024-08-11',NULL,4,7),
(8,'Completada','2024-08-08','2024-08-12','2024-08-11',4,8),
(9,'Pendiente','2024-08-09','2024-08-13',NULL,5,9),
(10,'Completada','2024-08-10','2024-08-14','2024-08-12',5,10);

-- Detalle de Órdenes
INSERT INTO detalle_ordenes (id_orden, id_producto, cantidad, precio_lista, descuento) VALUES
(1,1,2,1.50,0.00),
(1,2,1,0.99,0.00),
(2,3,3,0.95,0.05),
(2,4,2,1.20,0.00),
(3,5,1,2.50,0.10),
(4,6,1,899.99,0.15),
(5,7,1,599.99,0.10),
(6,8,5,5.99,0.00),
(7,9,1,999.99,0.20),
(8,10,1,349.99,0.05);