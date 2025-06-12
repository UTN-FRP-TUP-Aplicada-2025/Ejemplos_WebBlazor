
USE MASTER

GO

DROP DATABASE IF EXISTS Ejemplos_ASP_MVC_DB

GO

CREATE DATABASE  Ejemplos_ASP_MVC_DB

GO


USE Ejemplos_ASP_MVC_DB


GO

DROP TABLE IF EXISTS Personas
DROP TABLE IF EXISTS Usuarios_Roles
DROP TABLE IF EXISTS Usuarios
DROP TABLE IF EXISTS Roles

GO

CREATE TABLE Usuarios
(
	Nombre NVARCHAR(50) PRIMARY KEY NOT NULL,
	Clave NVARCHAR(200) NOT NULL,
);

CREATE TABLE Roles
(
	Nombre NVARCHAR(50) PRIMARY KEY NOT NULL,
);

CREATE TABLE  Usuarios_Roles
(
	Nombre_Usuario NVARCHAR(50) NOT NULL,
	Nombre_Rol NVARCHAR(50) NOT NULL,
	CONSTRAINT UQ_Usuarios_Roles UNIQUE (Nombre_Usuario, Nombre_Rol)
);

GO 

ALTER TABLE Usuarios_Roles
ADD CONSTRAINT FK_Usuarios_Roles_Roles
FOREIGN KEY (Nombre_Rol) REFERENCES Roles(Nombre);

ALTER TABLE Usuarios_Roles
ADD CONSTRAINT FK_Usuarios_Roles_Usuarios
FOREIGN KEY (Nombre_Usuario) REFERENCES Usuarios(Nombre);

GO

CREATE TABLE Personas
(
	Id INT IDENTITY(1,1),
	DNI INT NOT NULL UNIQUE,
	Nombre NVARCHAR(100) NOT NULL,
	Fecha_Nacimiento DATE,
	--
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
            ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
            OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];



GO

INSERT INTO Personas (DNI, Nombre, Fecha_Nacimiento) VALUES 
(35843243, 'Sebastian', '1990-01-01'),
(35327489, 'Esteban', '1990-01-01'),
(43323432, 'Luisa', '2000-01-05'),
(30798132, 'Teresa', '1999-03-26'),
(35555132, 'Eduardo', '1995-07-03'),
(26555132, 'Rosa', '1975-07-03'),
(28451182, 'Griselda', '1982-07-26'),
(28733932, 'Carina', '1982-07-23'),
(24254932, 'Arturo', '1963-06-02'),
(28374602, 'Andres', '1980-03-02'),
(30694152, 'Estefania', '1985-05-02'),
(45235754, 'Norberto', '2004-02-06'),
(32432223, 'Ricardo', '2000-02-06'),
(23432224, 'Aurelio', '2004-02-06'),
(37232632, 'Cesar', '1987-02-02'),
(37237202, 'David', '1987-02-02'),
(37232435, 'Patricia', '1987-02-02'),
(37232932, 'Anal�a', '1987-02-02'),
(32042032, 'Dolores', '1987-02-02'),
(34237032, 'Gustavo', '1987-02-02'),
(42232072, 'Marianela', '1987-02-02'),
(37234210, 'Andrea', '1987-02-02'),
(37238236, 'Rita', '1987-02-02'), 
(32432432, 'Gimena', '1987-02-02'),
(33132440, 'Manuel', '1981-07-23'),
(33432532, 'Renata', '1982-12-01');

INSERT INTO Usuarios(Nombre, Clave)
VALUES('Admin', '123'),
('Maxima', 'maxima'),
('Eduardo', 'eduardo'),
('Estefania', 'estefania');

INSERT INTO Roles(Nombre)
VALUES('Admin'),
('Encuestador'),
('Supervisor');

INSERT INTO Usuarios_Roles(Nombre_Usuario, Nombre_Rol)
VALUES
('Admin', 'Admin'),
('Maxima', 'Admin'),
('Maxima', 'Supervisor'),
('Eduardo', 'Admin'),
('Estefania', 'Supervisor'),
('Eduardo', 'Encuestador');

GO

select * from Personas;

select * from Usuarios;

select * from Usuarios_Roles;

GO

--roles de estafania

DECLARE @Usuario NVARCHAR(50)='Admin';
SELECT u_r.* 
FROM Usuarios_Roles u_r
WHERE UPPER(TRIM(u_r.Nombre_Usuario)) LIKE UPPER(TRIM(@Usuario))
      AND UPPER(TRIM(u_r.Nombre_Rol)) LIKE UPPER(TRIM('%'))