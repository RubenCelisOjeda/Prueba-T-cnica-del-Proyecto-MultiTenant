DECLARE @DatabaseName NVARCHAR(100);
SET @DatabaseName = '{DatabaseName}'; -- Reemplaza {DatabaseName} con el valor real

-- Verificamos si la base de datos existe
IF DB_ID(@DatabaseName) IS NULL
BEGIN
    -- Creamos la base de datos
    DECLARE @SqlQuery NVARCHAR(MAX);
    SET @SqlQuery = '
        CREATE DATABASE ' + QUOTENAME(@DatabaseName);
    
    EXEC sp_executesql @SqlQuery;
    PRINT 'La base de datos ha sido creada exitosamente.';

    -- Creamos las tablas sin cambiar la base de datos actual
    SET @SqlQuery = '
        USE ' + QUOTENAME(@DatabaseName) + ';

        CREATE TABLE Product (
            Id INT PRIMARY KEY IDENTITY(1,1),
            Name NVARCHAR(100),
            IdTenant NVARCHAR(100)
        );
    ';
    EXEC sp_executesql @SqlQuery;

    PRINT 'La tabla ha sido creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe.';
END
