# Trabajo-Practico-Programacion-4
Espacio de entrega de trabajo practico de Programación 4


ID cliente: 690680499707-orghegda69c7ifihb9cohrsuepig4e7p.apps.googleusercontent.com


secreto cliente: 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


rol 1 = admin: ver, editar, crear, eliminar
rol 2 = user: ver, comprar
rol 3 = superAdmin: ver, editar, crear, eliminar, comprar
Insert por si no funciona el .bak:

USE [ComprasContext];
GO

-- 1. Acciones (1..5)
SET IDENTITY_INSERT dbo.Acciones ON;
INSERT INTO dbo.Acciones (IdAccion, Descripcion)
SELECT v.IdAccion, v.Descripcion
FROM (VALUES
    (1, N'Ver'),
    (2, N'Crear'),
    (3, N'Editar'),
    (4, N'Eliminar'),
    (5, N'Comprar')
) AS v(IdAccion, Descripcion)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Acciones a WHERE a.IdAccion = v.IdAccion OR a.Descripcion = v.Descripcion
);
SET IDENTITY_INSERT dbo.Acciones OFF;
GO

-- 2. Roles (1..3)
SET IDENTITY_INSERT dbo.Roles ON;
INSERT INTO dbo.Roles (IdRol, Descripcion)
SELECT v.IdRol, v.Descripcion
FROM (VALUES
    (1, N'Admin'),
    (2, N'User'),
    (3, N'SuperAdmin')
) AS v(IdRol, Descripcion)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Roles r WHERE r.IdRol = v.IdRol OR r.Descripcion = v.Descripcion
);
SET IDENTITY_INSERT dbo.Roles OFF;
GO

-- 3. Productos 
SET IDENTITY_INSERT dbo.Productos ON;
INSERT INTO dbo.Productos (Id, Nombre, Precio, Cantidad, Borrado)
SELECT v.Id, v.Nombre, v.Precio, v.Cantidad, v.Borrado
FROM (VALUES
    (8,  N'banana',   CAST(100.00 AS decimal(18,2)),  8,  CAST(0 AS bit)),
    (9,  N'manzana',  CAST(500.00 AS decimal(18,2)),  5,  CAST(0 AS bit)),
    (10, N'pera',     CAST(1000.00 AS decimal(18,2)), 20, CAST(0 AS bit)),
    (11, N'palta',    CAST(2000.00 AS decimal(18,2)), 10, CAST(0 AS bit))
) AS v(Id, Nombre, Precio, Cantidad, Borrado)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Productos p WHERE p.Id = v.Id
);
SET IDENTITY_INSERT dbo.Productos OFF;
GO

SET IDENTITY_INSERT dbo.Usuarios ON;
INSERT INTO dbo.Usuarios (IdUsuario, Email, Nombre, FechaAlta)
SELECT v.IdUsuario, v.Email, v.Nombre, v.FechaAlta
FROM (VALUES
    (1, N'nehuenpessah88@gmail.com', N'nehuen',    CAST('2026-11-16T15:46:01.0777852' AS datetime2)),
    (2, N'nehuenpessah28@gmail.com', N'testeuser', CAST('2026-11-16T15:46:01.0867862' AS datetime2))
) AS v(IdUsuario, Email, Nombre, FechaAlta)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Usuarios u WHERE u.IdUsuario = v.IdUsuario OR u.Email = v.Email
);
SET IDENTITY_INSERT dbo.Usuarios OFF;
GO

-- 5. RolesAcciones
INSERT INTO dbo.RolesAcciones (IdRol, IdAccion)
SELECT v.IdRol, v.IdAccion
FROM (VALUES
    (1, 1), (1, 2), (1, 3), (1, 4), -- Admin
    (2, 1), (2, 5),                 -- User
    (3, 1), (3, 2), (3, 3), (3, 4), (3, 5) -- SuperAdmin
) AS v(IdRol, IdAccion)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.RolesAcciones ra WHERE ra.IdRol = v.IdRol AND ra.IdAccion = v.IdAccion
);
GO

-- 6. UsuarioRoles
INSERT INTO dbo.UsuarioRoles (IdUsuario, IdRol)
SELECT v.IdUsuario, v.IdRol
FROM (VALUES
    (1, 3), -- nehuen -> SuperAdmin
    (2, 2)  -- testeuser -> User
) AS v(IdUsuario, IdRol)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.UsuarioRoles ur WHERE ur.IdUsuario = v.IdUsuario AND ur.IdRol = v.IdRol
);
GO

