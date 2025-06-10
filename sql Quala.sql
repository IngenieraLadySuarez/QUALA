CREATE TABLE js_suc_sucursal (
    Id INT PRIMARY KEY IDENTITY,
    Codigo INT NOT NULL UNIQUE,
    Descripcion NVARCHAR(250) NOT NULL,
    Direccion NVARCHAR(250) NOT NULL,
    Identificacion NVARCHAR(50) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    MonedaId INT NOT NULL
);
---------------------------------------------------------
CREATE TABLE js_mon_moneda (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL
);
---------------------------------------------------------
CREATE PROCEDURE js_suc_InsertarSucursal
    @Codigo INT,
    @Descripcion NVARCHAR(250),
    @Direccion NVARCHAR(250),
    @Identificacion NVARCHAR(50),
    @FechaCreacion DATETIME,
    @MonedaId INT
AS
BEGIN
    INSERT INTO js_suc_sucursal (Codigo, Descripcion, Direccion, Identificacion, FechaCreacion, MonedaId)
    VALUES (@Codigo, @Descripcion, @Direccion, @Identificacion, @FechaCreacion, @MonedaId);
END
---------------------------------------------------------
CREATE PROCEDURE js_suc_ObtenerSucursales
AS
BEGIN
    SELECT * FROM js_suc_sucursal;
END
---------------------------------------------------------
CREATE PROCEDURE js_suc_ObtenerSucursalPorId
    @Id INT
AS
BEGIN
    SELECT * FROM js_suc_sucursal WHERE Id = @Id;
END
---------------------------------------------------------
CREATE PROCEDURE js_suc_ActualizarSucursal
    @Id INT,
    @Codigo INT,
    @Descripcion NVARCHAR(250),
    @Direccion NVARCHAR(250),
    @Identificacion NVARCHAR(50),
    @FechaCreacion DATETIME,
    @MonedaId INT
AS
BEGIN
    UPDATE js_suc_sucursal
    SET Codigo = @Codigo,
        Descripcion = @Descripcion,
        Direccion = @Direccion,
        Identificacion = @Identificacion,
        FechaCreacion = @FechaCreacion,
        MonedaId = @MonedaId
    WHERE Codigo = @Codigo;
END
---------------------------------------------------------
CREATE PROCEDURE js_suc_EliminarSucursal
    @Codigo INT
AS
BEGIN
    DELETE FROM js_suc_sucursal WHERE Codigo = @Codigo;
END
---------------------------------------------------------
CREATE PROCEDURE js_mon_ObtenerMonedas
AS
BEGIN
    SELECT * FROM js_mon_moneda;
END
---------------------------------------------------------
CREATE PROCEDURE js_mon_InsertarMoneda
    @Nombre NVARCHAR(250)
AS
BEGIN
    INSERT INTO js_mon_moneda (Nombre)
    VALUES (@Nombre);
END
---------------------------------------------------------
CREATE PROCEDURE js_mon_ObtenerMonedaPorId
    @Id INT
AS
BEGIN
    SELECT * FROM js_mon_moneda WHERE Id = @Id;
END
---------------------------------------------------------
CREATE PROCEDURE js_mon_ActualizarMoneda
    @Nombre NVARCHAR(250),
	@Id INT
AS
BEGIN
    UPDATE js_mon_moneda
    SET Nombre = @Nombre
    WHERE Id = @Id;
END
---------------------------------------------------------
CREATE PROCEDURE js_mon_EliminarMoneda
    @Id INT
AS
BEGIN
    DELETE FROM js_mon_moneda WHERE Id = @Id;
END
---------------------------------------------------------
ALTER PROCEDURE js_suc_ObtenerSucursalPorCod
    @Codigo INT
AS
BEGIN
    SELECT * FROM js_suc_sucursal WHERE Codigo = @Codigo;
END
